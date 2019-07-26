using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Microliu.IdentityServer
{
    public sealed class ConfigIdentity
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource
                {
                    Name ="emailService",
                    DisplayName="Email Service API",
                    Description="邮件服务接口",
                    UserClaims = new List<string>{JwtClaimTypes.Role},
                    ApiSecrets = new List<Secret>{new Secret("emailServiceSecret".Sha256())},
                    Scopes = new List<Scope>
                    {
                        new Scope("emailService.read"),
                        new Scope("emailService.write")
                    }
                },
                new ApiResource("smsService","SMS Service API")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "emailServiceClient",
                    ClientName="邮件服务",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret("emailServiceSecret".Sha256())
                    },
                    AllowedScopes =new List<string>{ "emailService.read"},
                    AccessTokenLifetime = 60*60*1,
                    RedirectUris = new List<string> {"https://localhost:10111/signin-oidc"},
                    PostLogoutRedirectUris = new List<string> { "https://localhost:10111" }
                },
                new Client
                {
                    ClientId = "smsServiceClient",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret("smsServiceClient".Sha256())
                    },
                    AllowedScopes =new List<string>{ "smsService" },
                    AccessTokenLifetime = 60*60*1
                }
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = Guid.NewGuid().ToString(),
                    Username = "liuzhuang",
                    Password ="liuzhuang",
                    Claims = new List<Claim> {
                        new Claim(JwtClaimTypes.Role,"superadmin"),
                        new Claim(JwtClaimTypes.Email,"liuzhuang@6iuu.com")
                    }
                },
                new TestUser
                {
                    SubjectId = Guid.NewGuid().ToString(),
                    Username="admin",
                    Password = "admin",
                    Claims = new List<Claim>(){new Claim(JwtClaimTypes.Role,"admin")}
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>();
        }
    }
}
