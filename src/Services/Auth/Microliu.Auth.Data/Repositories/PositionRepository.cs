using Microliu.Auth.DataMySQL.Interfaces;
using Microliu.Auth.Domain;
using Microliu.Auth.Domain.Entities;
using Microliu.Auth.Domain.Repositories;
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

        public IQueryable<Position> Get(string id)
        {
            return _entities.Where(e => e.Id == id);
        }
    }
}
