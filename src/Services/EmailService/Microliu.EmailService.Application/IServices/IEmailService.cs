using Microliu.EmailService.Domain.ViewModels;
using Microliu.Utils;
using System;

namespace Microliu.EmailService.Application.IServices
{
    public interface IEmailService
    {
        ReturnResult SendAsync(EmailSendDto input);

    }
}
