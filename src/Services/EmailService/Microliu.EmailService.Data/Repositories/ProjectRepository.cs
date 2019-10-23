using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microliu.EmailService.Data.Repositories;
using Microliu.EmailService.Domain;

namespace Microliu.EmailService.Data
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        public ProjectRepository(EmailDbContext ctx) : base(ctx)
        {
        }

        public Project GetEntity(long id)
        {
            return GetAll().Where(x => x.Deleted == Domain.Entities.Deleted.NotDelete)
                          .Where(x => x.Enabled == Domain.Entities.Enabled.Enabled)
                          .Where(x => x.Id == id)
                          .FirstOrDefault();
        }

        public Project GetEntityByName(long uid, long categoryId,string name)
        {
            return GetAll().Where(x => x.Deleted == Domain.Entities.Deleted.NotDelete)
                           .Where(x => x.Enabled == Domain.Entities.Enabled.Enabled)
                           .Where(x => x.User.Id==uid&&x.Name==name&&x.Category.Id==categoryId)
                           .FirstOrDefault();
        }
    }
}
