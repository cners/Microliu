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
            return _userRepository.GetEntity(id);
        }

        public async Task<bool> CreateUser(CreateUserModel input)
        {
            //var user = UserConverter.ToUser(input);

            var user = AutoMapperHelper.Map<User>(input);
            user.Id = Guid.NewGuid().ToString("N");

            if (_userRepository.Exists(input.UserName))
            {
                throw new AuthException("用户名已存在");
            }
            await _unitOfWork.Add<User>(user);
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}
