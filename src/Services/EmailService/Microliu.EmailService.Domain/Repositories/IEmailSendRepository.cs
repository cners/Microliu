using Microliu.EmailService.Domain.Entities;
using Microliu.EmailService.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microliu.EmailService.Domain.Repositories
{
    public interface IEmailSendRepository : IRepository<EmailSend>
    {
        EmailSend GetEntity(long id);
    }
}
