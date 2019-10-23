using System;
using System.Collections.Generic;
using System.Text;
using Microliu.EmailService.Domain.SeedWork;

namespace Microliu.EmailService.Domain.Repositories
{
    public interface IProjectCategoryRepository:IRepository<ProjectCategory>
    {
        ProjectCategory GetEntity(long categoryId);

        ProjectCategory GetEntityByName(long uid, string name);
    }
}
