using System;
using System.Collections.Generic;
using System.Text;
using Microliu.EmailService.Domain.SeedWork;

namespace Microliu.EmailService.Domain.Repositories
{
    public interface IBlackListRepository:IRepository<BlackList>
    {
        BlackList GetEntityByEmail(string email);
    }
}
