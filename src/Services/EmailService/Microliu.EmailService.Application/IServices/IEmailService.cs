using Microliu.EmailService.Application.ViewModel;
using Microliu.EmailService.Domain.ViewModels;
using Microliu.Utils;
using System;
using System.Collections.Generic;

namespace Microliu.EmailService.Application.IServices
{
    public interface IEmailService
    {
        ReturnResult SendAsync(EmailSendDto input);


        ReturnResult GetEmail(long emailId);

        List<EmailDto> GetEmailList(EmailLogQueryDto dto, out long total);
    }
}
