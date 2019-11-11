using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microliu.EmailService.Data.Repositories;
using Microliu.EmailService.Domain;
using Microliu.EmailService.Domain.Repositories;

namespace Microliu.EmailService.Data
{
    public class BlackListRepository : BaseRepository<BlackList>, IBlackListRepository
    {
        public BlackListRepository(EmailDbContext ctx) : base(ctx)
        {
        }

        public BlackList GetEntityByEmail(string email)
        {
            return GetAll().Where(x => x.Email == email).FirstOrDefault();
        }
    }
}
