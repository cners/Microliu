using Microliu.EmailService.Domain.Entities;
using Microliu.EmailService.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microliu.EmailService.Data.Repositories
{
    public class EmailSendRepository : BaseRepository<EmailSend>, IEmailSendRepository
    {
        public EmailSendRepository(EmailDbContext ctx)
            : base(ctx) { }

    }
}
