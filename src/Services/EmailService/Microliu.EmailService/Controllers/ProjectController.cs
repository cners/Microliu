using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microliu.EmailService.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microliu.EmailService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        /// <summary>
        /// 创建一个项目类别
        /// </summary>
        /// <param name="uid">用户标识</param>
        /// <param name="name">项目类别名称</param>
        /// <returns></returns>
        [HttpGet("CreateCategory/{uid}/{name}")]
        public async Task<IActionResult> CreateCategory(long uid, string name)
        {
            var r = await _projectService.CreateProjectCategory(uid, name);
            return Ok(r);
        }

        /// <summary>
        /// 创建一个项目
        /// </summary>
        /// <param name="uid">用户标识</param>
        /// <param name="categoryId">分类标识</param>
        /// <param name="projectName">项目名称</param>
        /// <returns></returns>
        [HttpGet("Create/{uid}/{categoryId}/{projectName}")]
        public async Task<IActionResult> Create(long uid,long categoryId, string projectName)
        {
            var r = await _projectService.CreateProject(uid, categoryId, projectName);
            return Ok(r);
        }
    }
}