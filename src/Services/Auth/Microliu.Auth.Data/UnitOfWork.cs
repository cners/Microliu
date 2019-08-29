using Microliu.Auth.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Remotion.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Microliu.Auth.DataMySQL
{
    public class UnitOfWork : IUnitOfWork
    {
        private AuthDbContext _context;
        private IDbContextTransaction _dbTransaction;

        public UnitOfWork(AuthDbContext ctx)
        {
            _context = ctx;
        }

        public void BeginTransaction()
        {
            _dbTransaction = _context.Database.BeginTransaction();
        }

        public async Task<bool> CommitAsync()
        {
            if (_dbTransaction != null)
            {
                _dbTransaction.Commit();
                return true;
            }
            else
            {
                return await _context.SaveChangesAsync() > 0;
            }
        }

        public async Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters)
        {
            return await _context.Database.ExecuteSqlCommandAsync(sql, parameters);
        }



        public async Task<bool> Remove<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Set<TEntity>().Remove(entity);
            if (_dbTransaction != null)
                return await _context.SaveChangesAsync() > 0;
            return true;
        }

        public async Task<bool> Modify<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Set<TEntity>().Update(entity);
            if (_dbTransaction != null)
                return await _context.SaveChangesAsync() > 0;
            return true;
        }

        public async Task<bool> Add<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Set<TEntity>().Add(entity);
            if (_dbTransaction != null)
                return await _context.SaveChangesAsync() > 0;
            return true;
        }

        public void Rollback()
        {
            if (_dbTransaction != null)
                _dbTransaction.Rollback();
        }

        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }
    }
}
