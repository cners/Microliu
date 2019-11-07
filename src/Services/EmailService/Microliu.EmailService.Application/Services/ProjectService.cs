using System.Linq;
using System.Threading.Tasks;
using Microliu.EmailService.Domain;
using Microliu.Utils;

namespace Microliu.EmailService.Application.Services
{
    public partial class EmailApplication : IProjectService
    {
        public async Task<ReturnResult> CreateProject(long uid, long categoryId, string name)
        {
            var userinfo = _userInfoRepository.GetEntity(uid);
            if (userinfo == null) return _return.SetMessage("用户信息无效");

            var category = _projectCategoryRepository.GetEntity(categoryId);
            if (category == null) return _return.SetMessage("项目分类信息无效");

            var projectInfo = _projectRepository.GetEntityByName(uid, categoryId, name);
            if (projectInfo != null) return _return.SetMessage("该项目名称已存在");

            Project project = new Project();
            project.SetCreateInfo(name);
            project.User = userinfo;
            project.Category = category;
            await _unitOfWork.Add(project);
            await _unitOfWork.CommitAsync();

            return _return.SetSuccess(true);
        }

        public async Task<ReturnResult> CreateProjectCategory(long uid, string name)
        {
            var userinfo = _userInfoRepository.GetEntity(uid);
            if (userinfo == null) return _return.SetMessage("用户信息无效");

            var category = _projectCategoryRepository.GetEntityByName(uid, name);
            if (category != null) return _return.SetMessage("该类别名称已存在");

            ProjectCategory projectCategory = new ProjectCategory();
            projectCategory.Name = name;
            projectCategory.Uid = uid;
            await _unitOfWork.Add(projectCategory);

            RkUserProCategory rk = new RkUserProCategory();
            rk.User = userinfo;
            rk.ProjectCategory = projectCategory;
            await _unitOfWork.Add(rk);

            await _unitOfWork.CommitAsync();

            return _return.SetSuccess(true);
        }

        public ProjectListDto GetProjects(ProjectQueryDto dto)
        {
            var returnProjects = new ProjectListDto();
            var projects = _projectRepository.GetAll();
            if (!string.IsNullOrEmpty(dto.ProjectName))
            {
                projects = projects.Where(x => x.Name.Contains(dto.ProjectName));
            }
            projects = projects.OrderByDescending(x => x.CreateTime);
            returnProjects.Total = projects.Count();
            returnProjects.Projects = projects.Skip((dto.Pagination - 1) * dto.PageSize).Take(dto.PageSize)
                                   .AsEnumerable()
                                   .Select(x =>
                                   {
                                       var node = new ProjectDto();
                                       node.Id = x.Id.ToString();
                                       node.Name = x.Name;
                                       node.CreateTime = x.CreateTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                                       var category  = _projectCategoryRepository.GetEntity(x.CategoryId);

                                       node.CategoryName = category?.Name ?? "";
                                       return node;
                                   }).ToList();
            return returnProjects;
        }
    }
}
