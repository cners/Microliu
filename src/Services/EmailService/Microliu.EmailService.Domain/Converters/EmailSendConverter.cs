using System;
using Microliu.EmailService.Domain.Entities;
using Microliu.EmailService.Domain.ViewModels;
using Microliu.Utils;

namespace Microliu.EmailService.Domain
{
    public class EmailSendConverter
    {
        public static EmailSend ToEmailSend(EmailSendDto input)
        {
            var send = new EmailSend();
            send.Id = SnowflakeId.Default().NextId();
            send.Subject = input.Subject;
            send.To = (input.To ?? "").Trim();
            send.CopyTo = (input.CopyTo ?? "").Trim();
            send.Body = input.Body;

            send.Enabled = Enabled.Enabled;
            send.Deleted = Deleted.NotDelete;
            send.CreateTime = DateTime.Now;

            send.Status = "sending";
            send.ErrorMessage = "";
            send.ProjectId = input.ProjectId;
            return send;
        }
    }
}
