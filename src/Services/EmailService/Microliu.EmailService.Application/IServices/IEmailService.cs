using Microliu.EmailService.Domain.ViewModels;
using System;

namespace Microliu.EmailService.Application.IServices
{
    public interface IEmailService
    {
        void SendAsync(EmailSendDto input);

    }
}
