using Microsoft.AspNetCore.Mvc;

namespace Microliu.Auth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet(nameof(Check))]
        public IActionResult Check()
        {
            return Ok();
        }
    }
}