using System.Linq;
using Microliu.EmailService.Data.Repositories;
using Microliu.EmailService.Domain;
using Microliu.EmailService.Domain.Repositories;

namespace Microliu.EmailService.Data
{
    public class ProjectCategoryRepository : BaseRepository<ProjectCategory>, IProjectCategoryRepository
    {
        public ProjectCategoryRepository(EmailDbContext ctx) : base(ctx)
        {
        }

        public ProjectCategory GetEntity(long categoryId)
        {
            return GetAll().Where(x => x.Deleted == Domain.Entities.Deleted.NotDelete)
                           .Where(x => x.Id == categoryId && x.Enabled == Domain.Entities.Enabled.Enabled)
                           .FirstOrDefault();
        }

        public ProjectCategory GetEntityByName(long uid, string name)
        {
            return GetAll().Where(x => x.Deleted == Domain.Entities.Deleted.NotDelete)
                          .Where(x => x.Uid == uid && x.Enabled == Domain.Entities.Enabled.Enabled)
                          .Where(x => x.Name == name)
                          .FirstOrDefault();
        }
    }
}
