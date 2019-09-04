using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microliu.EmailService.Domain.SeedWork
{
   public interface IRepository<T> where T:IAggregateRoot
    {

        T GetEntity(string id);

        IQueryable<T> GetAll();
    }
}
