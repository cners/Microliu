using System.Threading.Tasks;
using Microliu.EmailService.Application.ViewModel;
using Microliu.EmailService.Domain.Entities;
using Microliu.Utils;

namespace Microliu.EmailService.Application.Services
{
    public partial class EmailApplication : IUserService
    {
        public async Task<ReturnResult> Create(string email, string password)
        {
            UserInfo userInfo = new UserInfo();
            userInfo.SetUserInfo(email ?? "", password ?? "");

            if (string.IsNullOrEmpty(userInfo.Email)) return _return.SetMessage("邮箱不能为空");
            if (!userInfo.Email.Contains("@")) return _return.SetMessage("邮箱地址不正确");
            if (string.IsNullOrEmpty(userInfo.Password)) return _return.SetMessage("密码不能为空");
            if (userInfo.Password.Length < 6) return _return.SetMessage("密码不能低于6位");

            if (_userInfoRepository.GetEntityByEmail(email) != null)
            {
                return _return.SetMessage("该邮箱已存在");
            }

            await _unitOfWork.Add(userInfo);
            await _unitOfWork.CommitAsync();
            return _return.SetSuccess(true);
        }

        public UserEntityDto GetUserInfo(long uid)
        {
            var userinfo = _userInfoRepository.GetEntity(uid);
            if (userinfo == null) return null;
            UserEntityDto dto = new UserEntityDto();
            dto.Email = userinfo.Email;
            dto.Id = userinfo.Id.ToString();
            return dto;
        }

        public ReturnResult Login(UserLoginDto dto)
        {
            if (dto == null) _return.SetMessage("参数无效");

            var userinfo = _userInfoRepository.Login(dto.Email, dto.Password);
            if (userinfo == null) return _return.SetMessage("用户名或密码有误");
            UserEntityDto r = new UserEntityDto();
            r.Email = userinfo.Email;
            r.Id = userinfo.Id.ToString();
            return _return.SetData(true, r);
        }

        public async Task<ReturnResult> ChangeEmail(string email, string password, string newEmail)
        {

            var emailDbData = _userInfoRepository.Login(email, password);
            if (emailDbData == null) return _return.SetMessage("密码有误");

            if (string.IsNullOrEmpty(newEmail)) return _return.SetMessage("新邮箱不能为空");
            if (!newEmail.Contains("@")) return _return.SetMessage("邮箱地址不正确");
            var newEmailDbData = _userInfoRepository.GetEntityByEmail(newEmail);
            if (newEmailDbData != null) return _return.SetMessage("新邮箱已存在");

            emailDbData.Email = newEmail;
            await _unitOfWork.Modify(emailDbData);
            await _unitOfWork.CommitAsync();
            return _return.SetSuccess(true);
        }
    }
}
