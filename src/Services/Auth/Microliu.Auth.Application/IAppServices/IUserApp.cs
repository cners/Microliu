using Microliu.Auth.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Microliu.Auth.Application
{
    public partial interface IAuthApplication
    {
        // 用户
        dynamic GetUser(string id);

        dynamic GetUsers(string positionId);


        Task<bool> CreateUser(CreateUserModel input);
    }
}
