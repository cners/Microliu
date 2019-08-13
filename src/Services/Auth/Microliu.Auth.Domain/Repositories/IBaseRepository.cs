using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microliu.Auth.Domain
{
    public partial interface IBaseRepository<T> : IDisposable
    {
        Task<T> AddAsync(T newEntity, CancellationToken ct = default(CancellationToken));
    }
}
