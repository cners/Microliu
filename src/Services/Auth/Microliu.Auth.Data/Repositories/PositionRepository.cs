using Microliu.Auth.Domain;
using Microliu.Auth.Domain.Entities;
using Microliu.Auth.Domain.Repositories;
using Microliu.Auth.Domain.SeedWork;
using Microliu.Auth.Domain.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microliu.Auth.DataMySQL
{
    public class PositionRepository : BaseRepository<Position>, IPositionRepository
    {
        public PositionRepository(AuthDbContext ctx)
            : base(ctx) { }

        public Position Add(Position position)
        {
            return _context.position.Add(position).Entity;
        }

        public void Update(Position position)
        {
            _context.Entry(position).State = EntityState.Modified;
        }

        public IQueryable<Position> GetPositions(SearchPositionModel input)
        {
            var positions = GetAll();
            int count = (input.PageIndex - 1) * input.PageSize;
            if (!string.IsNullOrEmpty(input.Name))
            {
                positions = positions.Where(e => e.Name == input.Name);
            }
            return positions.OrderByDescending(e => e.CreateTime)
                .Skip(count).Take(input.PageSize);
        }
    }
}
