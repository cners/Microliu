using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microliu.Auth.Domain
{
    public partial interface IBaseRepository<T> : IDisposable
    {
        DbType GetDbType();

        Task<T> AddAsync(T newEntity, CancellationToken ct = default(CancellationToken));

        Task SaveChangesAsync();

        Task RemoveAsync(string id, CancellationToken ct = default(CancellationToken));
    }
}
