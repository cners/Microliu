using DotNetCore.CAP;
using Hangfire;
using MailKit.Net.Smtp;
using Microliu.EmailService.Application.ViewModel;
using Microliu.EmailService.Domain;
using Microliu.EmailService.Domain.Entities;
using Microliu.EmailService.Domain.ViewModels;
using Microliu.Utils;
using MimeKit;
using MimeKit.Text;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Microliu.EmailService.Application.Services
{
    public partial class EmailApplication
    {
        [CapSubscribe("microliu.email.send")]
        public ReturnResult SendAsync(EmailSendDto input)
        {
            _logger.TraceBuilder($"[microliu.email.send] [receive] [data:{JsonConvert.SerializeObject(input)}]")
                   .AddObject(input)
                   .AddTags("email", "send", "params")
                   .Submit();

            var dto = EmailSendConverter.ToEmailSend(input);
            dto.From = _emailSettings.Value.Sender.Name ?? "liu.zhuang@lzassist.com";
            _unitOfWork.Add(dto);
            _unitOfWork.CommitAsync();

            // 黑名单检查
            var checkToEmails = BlackEmailCheck(dto.To.Split(';'));
            if (!string.IsNullOrEmpty(checkToEmails))
            {
                dto.SetSendError($"收件人{checkToEmails}在黑名单，无法接收");
                _unitOfWork.Modify<EmailSend>(dto);
                _unitOfWork.CommitAsync();

                return ReturnResult.Set(false, $"发送失败,{dto.ErrorMessage}");
            }
            var checkCopyEmails = BlackEmailCheck(dto.CopyTo.Split(';'));
            if (!string.IsNullOrEmpty(checkCopyEmails))
            {
                dto.SetSendError($"抄送人{checkCopyEmails}在黑名单，无法接收");
                _unitOfWork.Modify<EmailSend>(dto);
                _unitOfWork.CommitAsync();

                return ReturnResult.Set(false, $"发送失败,{dto.ErrorMessage}");
            }
            // 发送人
            var password = _emailSettings.Value.Sender.Password ?? "";
            var smtpHost = _emailSettings.Value.Sender.Host ?? "";
            var smtpPort = _emailSettings.Value.Sender.Port;

            try
            {
                // 构建发送信息
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(dto.From));
                message.Subject = dto.Subject;

                var multiPart = new Multipart("mixed");
                var textPart = new TextPart(TextFormat.Html) { Text = dto.Body };
                multiPart.Add(textPart);
                message.Body = multiPart;

                message.To.Add(new MailboxAddress(dto.To));
                if (!string.IsNullOrEmpty(dto.CopyTo))
                {
                    message.Cc.Add(new MailboxAddress(dto.CopyTo));
                }

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    using (var cts = new CancellationTokenSource(60000))//60s
                    {

                        if (dto.From.ToLower().Trim().EndsWith("qq.com"))
                        {
                            client.Connect(smtpHost, smtpPort, MailKit.Security.SecureSocketOptions.Auto, cts.Token);
                        }
                        else
                        {
                            client.Connect(smtpHost, smtpPort, MailKit.Security.SecureSocketOptions.None, cts.Token);
                        }
                        //client.AuthenticationMechanisms.Remove("XOAUTH2");

                        // Note: only needed if the SMTP server requires authentication
                        client.Authenticate(dto.From, password);
                        try
                        {
                            client.Send(message);
                        }
                        catch (SmtpCommandException ex)
                        {
                            // 发送失败，记录失败信息
                            if (ex.ErrorCode == SmtpErrorCode.MessageNotAccepted)
                            {
                                dto.SetSendError("MessageNotAccepted");
                            }
                            else if (ex.ErrorCode == SmtpErrorCode.SenderNotAccepted)
                            {
                                dto.SetSendError("MessageNotAccepted");//无此邮箱
                            }
                            else if (ex.ErrorCode == SmtpErrorCode.RecipientNotAccepted)
                            {
                                dto.SetSendError("MessageNotAccepted");
                            }
                            else
                            {
                                dto.SetSendError($"Send Unkonw Fail,{ex.Message}");
                            }

                            _unitOfWork.Modify<EmailSend>(dto);
                            _unitOfWork.CommitAsync();
                            return ReturnResult.Set(false, $"{dto.ErrorMessage}");
                        }
                        client.Disconnect(true);
                    }
                }

                // 发送成功，变更状态
                dto.Status = "success";
                _unitOfWork.Modify<EmailSend>(dto);
                _unitOfWork.CommitAsync();

                _logger.TraceBuilder($"[{this.GetType().FullName}] [microliu.email.send] [send] [storage]  [data:{JsonConvert.SerializeObject(input)}]")
                       .AddObject(dto)
                       .AddTags("microliu.email.send", "send", "storage", "email")
                       .Submit();
                return ReturnResult.Set(true, "", dto.Id);
            }
            catch (Exception ex)
            {
                // 连接失败，记录失败信息
                _logger.ToException(ex).AddObject(input).AddTags("microliu.email.send", "exception");
                _logger.Error(ex.Message, "microliu.email.send", "exception");

                dto.SetSendError(ex.Message);
                _unitOfWork.Modify<EmailSend>(dto);
                _unitOfWork.CommitAsync();

                return ReturnResult.Set(false, $"发送失败：{dto.ErrorMessage}");
            }
        }

        //public void SendRetry()
        //{
        //    //_jobs.Schedule(() => _logger.Debug($"我是10s后触发的"), TimeSpan.FromSeconds(10));
        //    BackgroundJob.Schedule(() => _logger.Debug($"我是10s后触发的"), TimeSpan.FromSeconds(10));
        //}

        //[CapSubscribe("microliu.email.send")]
        //public void Test(EmailSendDto input)
        //{
        //    //_logger.Debug($"请在3秒后通知我");
        //    //BackgroundJob.Schedule(() => _logger.Debug($"我是3s后触发的"), TimeSpan.FromSeconds(3));

        //    _logger.Debug($"开始执行周期任务");
        //    var jobId = "TestJob1";
        //    RecurringJob.AddOrUpdate(jobId, () => _logger.Debug($"我1行了22222222"), Cron.Minutely);
        //    //RecurringJob.RemoveIfExists(jobId);// 移除一个周期任务
        //    RecurringJob.Trigger(jobId);// 立即执行周期任务
        //}

        public ReturnResult GetEmail(long emailId)
        {
            var email = _emailSendRepository.GetEntity(emailId);
            if (email == null) return ReturnResult.Set(false, "无法获取该邮件信息");

            EmailDto ed = EmailDto.ConvertToEmailDto(email);
            return ReturnResult.Set(true, "", ed);
        }

        public List<EmailDto> GetEmailList(EmailLogQueryDto dto, out long total)
        {
            total = 0;
            var emails = _emailSendRepository.GetAll().Where(x => x.Deleted == Deleted.NotDelete)
                                             .Where(x=>x.ProjectId==long.Parse(dto.ProjectId));
            total = emails.LongCount();
            emails = emails.OrderByDescending(x => x.CreateTime)
                           .Skip((dto.Pagination - 1) * dto.PageSize)
                           .Take(dto.PageSize);
            var list = emails.AsEnumerable()
                             .Select(x => EmailDto.ConvertToEmailDto(x))
                             .ToList();
            return list;
        }
    }
}
