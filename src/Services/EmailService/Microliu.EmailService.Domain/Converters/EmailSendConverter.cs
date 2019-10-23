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
            var emailSend = new EmailSend();
            emailSend.Id = SnowflakeId.Default().NextId();
            emailSend.Subject = input.Subject;
            emailSend.To = input.To;
            emailSend.CopyTo = input.CopyTo;
            emailSend.Body = input.Body;

            emailSend.Enabled = Enabled.Disabled;
            emailSend.Deleted = Deleted.NotDelete;
            emailSend.CreateTime = DateTime.Now;
            return emailSend;
        }
    }
}
