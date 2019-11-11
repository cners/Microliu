using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microliu.EmailService.Application.ViewModel;
using Microliu.Utils;

namespace Microliu.EmailService.Application
{
    public interface IUserService
    {
        Task<ReturnResult> Create(string email, string password);

        UserEntityDto GetUserInfo(long uid);

        ReturnResult Login(UserLoginDto dto);

        Task<ReturnResult> ChangeEmail(string email, string password, string newEmail);
    }
}
