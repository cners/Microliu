using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Microliu.IdentityServer
{
    public sealed class ConfigIdentity
    {
        /// <summary>
        /// 返回应用列表
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource
                {
                    Name ="emailApi",
                    DisplayName="Email Service API",
                    Description="邮件服务接口",
                    UserClaims = new List<string>{JwtClaimTypes.Role},
                    ApiSecrets = new List<Secret>{new Secret("emailServiceSecret".Sha256())},
                    Scopes = new List<Scope>
                    {
                        new Scope("emailApi")
                    }
                },
                 new ApiResource
                {
                    Name ="smsApi",
                    DisplayName="SMS Service API",
                    Description="短信服务接口",
                    UserClaims = new List<string>{JwtClaimTypes.Role},
                    ApiSecrets = new List<Secret>{new Secret("smsServiceSecret".Sha256())},
                    Scopes = new List<Scope>
                    {
                        new Scope("smsApi")
                    }
                }
            };
        }

        /// <summary>
        /// 返回账号列表
        /// （可存入数据库）
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "emailServiceClientId",
                    ClientName="邮件服务",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret("123456".Sha256())//秘钥
                    },
                    AllowedScopes ={ "emailApi", "smsApi"},//这个账号支持访问哪些应用
                    AccessTokenLifetime = 60*60*1,
                    RedirectUris = new List<string> {"https://localhost:10111/signin-oidc"},
                    PostLogoutRedirectUris = new List<string> { "https://localhost:10111" }
                },
                new Client
                {
                    ClientId = "smsServiceClientId",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword, 
                    ClientSecrets =
                    {
                        new Secret("123456".Sha256())//秘钥
                    },
                    AllowedScopes ={ "smsApi"},
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
