using Microliu.EmailService.Domain.Entities;
using Microliu.EmailService.Domain.ViewModels;
using Microliu.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microliu.EmailService.Domain
{
   public class EmailSendConverter
    {
        public static EmailSend ToEmailSend(EmailSendDto input)
        {
            var send = new EmailSend();
            send.Id = SnowflakeId.Default().NextId();
            send.Subject = input.Subject;
            send.To = input.To;
            send.CopyTo = input.CopyTo;
            send.Body = input.Body;

            send.Enabled = Enabled.Enabled;
            send.Deleted = Deleted.NotDelete;
            send.CreateTime = DateTime.Now;

            send.Status = "sending";
            send.ErrorMessage = "";
            return send;
        }
    }
}
