using System.Threading.Tasks;
using Microliu.Utils;

namespace Microliu.EmailService.Application
{
    public interface IProjectService
    {
        Task<ReturnResult> CreateProject(long uid,long categoryId, string name);

        Task<ReturnResult> CreateProjectCategory(long uid, string name);
    }
}
