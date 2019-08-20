using Microliu.Auth.Domain.Converters;
using Microliu.Auth.Domain.Entities;
using Microliu.Auth.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Microliu.Auth.Application
{
    public partial class AuthApplication
    {
        public dynamic GetUser(string id)
        {
            return _userRepos.Get(id);
        }

        public async Task<string> CreateUser(CreateUserModel input)
        {
            var user = UserConverter.ToUser(input);
            await _unitOfWork.RegisterNew<User>(user);
            await _unitOfWork.CommitAsync();
            return user.Id;
        }
    }
}
