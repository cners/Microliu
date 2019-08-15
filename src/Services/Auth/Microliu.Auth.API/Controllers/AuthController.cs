using Microliu.Auth.Application;
using Microliu.Auth.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Microliu.Auth.API.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthAppService _authAppService;

        public AuthController(IAuthAppService authAppService)
        {
            _authAppService = authAppService;
        }

        // POST: api/auth/createrole
        [MapToApiVersion("1.0")]
        [HttpGet(nameof(CreateRole))]
        public async Task<IActionResult> CreateRole()
        {
            var role = new Role
            {
                Id = Guid.NewGuid().ToString("N"),
                IsDeleted = 1,
                IsEnabled = 1,
                CreateTime = DateTimeOffset.Now,
                //CreatorId = "creatorid",
                //Creator = "Microliu",
                RoleName = "TestRoleName"
            };
            await _authAppService.CreateRole(role);
            return Ok("CreateRole");
        }

        [MapToApiVersion("2.0")]
        [HttpGet(nameof(Query))]
        public IActionResult Query()
        {
            return Ok("query");
        }
    }
}