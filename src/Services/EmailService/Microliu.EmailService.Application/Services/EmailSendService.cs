﻿using DotNetCore.CAP;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microliu.EmailService.Domain;
using Microliu.EmailService.Domain.Entities;
using Microliu.EmailService.Domain.ViewModels;
using MimeKit;
using MimeKit.Text;
using Newtonsoft.Json;
using Pomelo.AspNetCore.TimedJob;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microliu.EmailService.Application.Services
{
    public partial class EmailApplication
    {
        [CapSubscribe("microliu.email.send")]
        public void SendAsync(EmailSendDto input)
        {
            _logger.Trace("microliu.email.send[receive]: " + JsonConvert.SerializeObject(input));

            var message = EmailSendConverter.ToEmailSend(input);
            message.From = _emailSettings.Value.Sender.Name ?? "liu.zhuang@lzassist.com";
            _unitOfWork.Add<EmailSend>(message);
            _unitOfWork.CommitAsync();


            var password = _emailSettings.Value.Sender.Password ?? "";
            var host = _emailSettings.Value.Sender.Host ?? "";
            var port = _emailSettings.Value.Sender.Port;
            // 
            try
            {
                MimeMessage mime = new MimeMessage();
                mime.From.Add(new MailboxAddress(message.From));
                mime.Subject = message.Subject;

                var multiPart = new Multipart("mixed");

                //if (input.ContentType == EmailSendDto.ContentTYPE.HTML)
                {
                    var textPart = new TextPart(TextFormat.Html) { Text = message.Body };
                    multiPart.Add(textPart);
                }
                //else
                //{
                //    var textPart = new TextPart(TextFormat.Plain) { Text = message.Body };
                //    multiPart.Add(textPart);
                //}
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
                _logger.Trace("microliu.email.send[storage successed]");
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }


        }


        [Invoke(Begin = "2019-09-05 19:00", Interval = 100, SkipWhileExecuting = true)]
        public void TickReSend()
        {
            var output = "我是随机的小job" + (new Random()).Next(100);
            _logger.Debug(output);

            GC.Collect();
        }
    }
}