using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Microliu.EmailService.Domain.SeedWork
{
    public interface IUnitOfWork:IDisposable
    {
        Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters);

        Task<bool> CommitAsync();
        Task<bool> Remove<TEntity>(TEntity entity) where TEntity : class;
        Task<bool> Add<TEntity>(TEntity entity) where TEntity : class;
        Task<bool> Modify<TEntity>(TEntity entity) where TEntity : class;
        void Rollback();
    }
}
