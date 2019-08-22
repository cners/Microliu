using Microliu.Auth.Domain.Entities;
using Microliu.Auth.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microliu.Auth.Domain
{
    public interface IPositionRepository :  IBaseRepository<Position>
    {
        IQueryable<Position> GetPositions(SearchPositionModel input);
    }
}
