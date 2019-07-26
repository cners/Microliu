using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microliu.EmailService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {

        [HttpPost]
        public async Task<string> Login(UserRequestModel userRequestModel)
        {
            // discovery endpoints from metadata
            var client = new HttpClient();
            DiscoveryResponse disc = await client.GetDiscoveryDocumentAsync("http://localhost:10111");
            if (disc.IsError)
            {
                return "认证服务器未启动";
            }

            // request token
            TokenResponse tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disc.TokenEndpoint,
                ClientId = "emailServiceClient",
                ClientSecret = "emailServiceSecret",
                UserName = userRequestModel.Name,
                Password = userRequestModel.Password
            });

            //if (!tokenResponse.IsError)
            //{
            //    client.SetBearerToken(tokenResponse.AccessToken);
            //    var response = await client.GetAsync("http://localhost:10111/identity");
            //    if (!response.IsSuccessStatusCode)
            //    {
            //        Console.WriteLine(response.StatusCode);
            //        Console.WriteLine("没有权限访问");
            //        return "没有权限访问";
            //    }
            //}

            return tokenResponse.IsError ? tokenResponse.Error : tokenResponse.Json.ToString();
        }
    }



    public class UserRequestModel
    {
        [Required(ErrorMessage = "用户名不能为空")]
        public string Name { get; set; }


        [Required(ErrorMessage = "密码不能为空")]
        public string Password { get; set; }
    }
}