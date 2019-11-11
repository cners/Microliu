using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microliu.Auth.Domain.SeedWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters);

        //Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        //Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> CommitAsync();
        Task<bool> Remove<TEntity>(TEntity entity) where TEntity : class;
        Task<bool> Add<TEntity>(TEntity entity) where TEntity : class;
        Task<bool> Modify<TEntity>(TEntity entity) where TEntity : class;
        void Rollback();
    }
}
