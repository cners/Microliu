using System;
using System.Collections.Generic;
using System.Text;
using Microliu.EmailService.Domain.SeedWork;

namespace Microliu.EmailService.Domain
{
    public interface IProjectRepository : IRepository<Project>
    {
        Project GetEntity(long id);

        Project GetEntityByName(long uid,long categoryId, string name);

    }
}
