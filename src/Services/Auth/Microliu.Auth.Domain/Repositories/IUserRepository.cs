using Microliu.Auth.Domain.Entities;
using Microliu.Auth.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microliu.Auth.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        IQueryable<User> Query(string id, string positionId);

        bool Exists(string userName);

        User Add(User user);
    }
}
