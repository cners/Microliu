using Microliu.Auth.DataMySQL.Interfaces;
using Microliu.Auth.Domain;
using Microliu.Auth.Domain.Entities;
using Microliu.Auth.Domain.Repositories;
using Microliu.Auth.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microliu.Auth.DataMySQL
{
    public class PositionRepository : BaseRepository<Position>, IPositionRepository
    {
        public PositionRepository(IDbContext dbContext, IUnitOfWork unitOfWork, AuthDbContext authDbContext)
            : base(dbContext, unitOfWork, authDbContext)
        {
        }

        public IQueryable<Position> GetPositions(SearchPositionModel input)
        {
            int count = (input.PageIndex - 1) * input.PageSize;
            if (!string.IsNullOrEmpty(input.Name))
            {
                _entities = _entities.Where(e => e.Name == input.Name);
            }
            return _entities.OrderByDescending(e => e.CreateTime)
                .Skip(count).Take(input.PageSize);
        }

        //public Position GetEntity(string id)
        //{
        //    return _entities.Where(e => e.Id == id).FirstOrDefault();
        //}
    }
}
