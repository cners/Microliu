using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microliu.EmailService.Domain.Entities;
using Microliu.EmailService.Domain.SeedWork;

namespace Microliu.EmailService.Domain.Repositories
{
    public interface IUserInfoRepository : IRepository<UserInfo>
    {
        UserInfo GetEntity(long uid);
        UserInfo GetEntityByEmail(string email);
        UserInfo Login(string email, string password);


    }
}
