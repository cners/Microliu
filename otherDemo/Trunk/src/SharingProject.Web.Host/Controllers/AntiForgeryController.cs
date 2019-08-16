using Microsoft.AspNetCore.Antiforgery;
using SharingProject.Controllers;

namespace SharingProject.Web.Host.Controllers
{
    public class AntiForgeryController : SharingProjectControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
