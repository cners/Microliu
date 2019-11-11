using Microliu.EmailService.Domain.Entities;
using Microliu.EmailService.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microliu.EmailService.Data.Repositories
{
    public class EmailSendRepository : BaseRepository<EmailSend>, IEmailSendRepository
    {
        public EmailSendRepository(EmailDbContext ctx)
            : base(ctx) { }

        public EmailSend GetEntity(long id)
        {
            return
            GetAll().Where(x => x.Deleted == Deleted.NotDelete)
                    .Where(x => x.Id == id)
                    .FirstOrDefault();
        }
    }
}
