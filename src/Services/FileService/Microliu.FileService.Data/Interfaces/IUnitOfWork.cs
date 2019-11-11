using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Microliu.FileService.Data
{
    public interface IUnitOfWork
    {
        void BeginTransaction();

        Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters);

        Task<bool> RegisterNew<TEntity>(TEntity entity) where TEntity : class;

        Task<bool> RegisterUpdate<TEntity>(TEntity entity) where TEntity : class;

        Task<bool> RegisterClean<TEntity>(TEntity entity) where TEntity : class;

        Task<bool> RegisterDeleted<TEntity>(TEntity entity) where TEntity : class;

        Task<bool> CommitAsync();

        void Rollback();
    }
}
