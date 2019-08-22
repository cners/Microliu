using Microliu.Auth.Infrastructure;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microliu.Auth.Domain
{
    public partial interface IBaseRepository<T> : IDisposable
    {
        DbType GetDbType();

        T GetEntity(string id);

        IQueryable<T> GetAll();
    }
}
