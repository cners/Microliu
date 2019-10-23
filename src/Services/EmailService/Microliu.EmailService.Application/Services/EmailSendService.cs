using DotNetCore.CAP;
using Hangfire;
using MailKit.Net.Smtp;
using Microliu.EmailService.Domain;
using Microliu.EmailService.Domain.Entities;
using Microliu.EmailService.Domain.ViewModels;
using MimeKit;
using MimeKit.Text;
using Newtonsoft.Json;
using System;

namespace Microliu.EmailService.Application.Services
{
    public partial class EmailApplication
    {
        [CapSubscribe("microliu.email.send")]
        public void SendAsync(EmailSendDto input)
        {
            _logger.TraceBuilder($"[microliu.email.send] [receive] [data:{JsonConvert.SerializeObject(input)}]")
                   .AddObject(input)
                   .AddTags("email", "send", "params")
                   .Submit();

            var message = EmailSendConverter.ToEmailSend(input);
            message.From = _emailSettings.Value.Sender.Name ?? "liu.zhuang@lzassist.com";
            _unitOfWork.Add<EmailSend>(message);
            _unitOfWork.CommitAsync();

            // 发送人
            var password = _emailSettings.Value.Sender.Password ?? "";
            var host = _emailSettings.Value.Sender.Host ?? "";
            var port = _emailSettings.Value.Sender.Port;
            
            try
            {
                // 构建发送信息
                MimeMessage mime = new MimeMessage();
                mime.From.Add(new MailboxAddress(message.From));
                mime.Subject = message.Subject;

                var multiPart = new Multipart("mixed");
                var textPart = new TextPart(TextFormat.Html) { Text = message.Body };
                multiPart.Add(textPart);
                mime.Body = multiPart;


                mime.To.Add(new MailboxAddress(message.To));
                if (!string.IsNullOrEmpty(message.CopyTo))
                {
                    mime.Cc.Add(new MailboxAddress(message.CopyTo));
                }

                using (var smtp = new SmtpClient())
                {
                    smtp.Connect(host, port, false);
                    smtp.Authenticate(message.From, password);
                    smtp.Send(mime);
                    smtp.Disconnect(true);
                }

                message.Enabled = Enabled.Enabled;
                _unitOfWork.Modify<EmailSend>(message);
                _unitOfWork.CommitAsync();
                _logger.TraceBuilder($"[{this.GetType().FullName}] [microliu.email.send] [send] [storage]  [data:{JsonConvert.SerializeObject(input)}]")
                       .AddObject(message)
                       .AddTags("microliu.email.send", "send", "storage", "email")
                       .Submit();

                //SendRetry();
            }
            catch (Exception ex)
            {
                _logger.ToException(ex).AddObject(input).AddTags("microliu.email.send", "exception");
                _logger.Error(ex.Message, "microliu.email.send", "exception");
            }
        }



        public void SendRetry()
        {
            //_jobs.Schedule(() => _logger.Debug($"我是10s后触发的"), TimeSpan.FromSeconds(10));
            BackgroundJob.Schedule(() => _logger.Debug($"我是10s后触发的"), TimeSpan.FromSeconds(10));
        }

        [CapSubscribe("microliu.email.send")]
        public void Test(EmailSendDto input)
        {
            //_logger.Debug($"请在3秒后通知我");
            //BackgroundJob.Schedule(() => _logger.Debug($"我是3s后触发的"), TimeSpan.FromSeconds(3));

            _logger.Debug($"开始执行周期任务");
            var jobId = "TestJob1";
            RecurringJob.AddOrUpdate(jobId, () => _logger.Debug($"我1行了22222222"), Cron.Minutely);
            //RecurringJob.RemoveIfExists(jobId);// 移除一个周期任务
            RecurringJob.Trigger(jobId);// 立即执行周期任务
        }
    }
}
