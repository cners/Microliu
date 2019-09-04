using Microliu.EmailService.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microliu.EmailService.Application.IServices
{
    public interface IEmailService
    {

        void SendAsync(EmailSendDto input);
    }
}
