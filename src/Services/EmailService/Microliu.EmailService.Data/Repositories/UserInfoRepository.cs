using System;
using System.Linq;
using Microliu.EmailService.Domain.Entities;
using Microliu.EmailService.Domain.Repositories;
using UserInfo = Microliu.EmailService.Domain.Entities.UserInfo;

namespace Microliu.EmailService.Data.Repositories
{
    public class UserInfoRepository : BaseRepository<UserInfo>, IUserInfoRepository
    {
        public UserInfoRepository(EmailDbContext ctx) : base(ctx)
        {
        }

        public UserInfo GetEntity(long uid)
        {
            return GetAll().Where(x => x.Deleted == Domain.Entities.Deleted.NotDelete)
                    .Where(x => x.Id == uid)
                    .FirstOrDefault();
        }

        public UserInfo GetEntityByEmail(string email)
        {
            return GetAll().Where(x => x.Deleted == Domain.Entities.Deleted.NotDelete)
                    .Where(x => x.Email == email)
                    .FirstOrDefault();
        }

        public UserInfo Login(string email, string password)
        {
            return GetAll().Where(x => x.Deleted == Domain.Entities.Deleted.NotDelete)
                    .Where(x => x.Email == email && x.Password == password)
                    .FirstOrDefault();
        }
    }
}
