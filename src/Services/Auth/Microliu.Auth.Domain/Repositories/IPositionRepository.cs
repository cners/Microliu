using Microliu.Auth.Domain.Entities;
using Microliu.Auth.Domain.SeedWork;
using Microliu.Auth.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microliu.Auth.Domain
{
    public interface IPositionRepository : IRepository<Position>
    {
        Position Add(Position position);

        IQueryable<Position> GetPositions(SearchPositionModel input);
    }
}
