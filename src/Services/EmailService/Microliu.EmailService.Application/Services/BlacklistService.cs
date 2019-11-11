using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microliu.EmailService.Application.IServices;
using Microliu.EmailService.Application.ViewModel;
using Microliu.EmailService.Domain;
using Microliu.Utils;

namespace Microliu.EmailService.Application.Services
{
    public partial class EmailApplication : IBlacklistService
    {
        public async Task<ReturnResult> CreateBlacklist(string email)
        {
            if (string.IsNullOrEmpty(email)) return _return.SetMessage("邮箱不可为空");
            var emailDbData = _blackListRepository.GetEntityByEmail(email);
            if (emailDbData != null) return _return.SetMessage("已存在于黑名单中");

            BlackList blackList = new BlackList();
            blackList.Email = email;
            await _unitOfWork.Add(blackList);
            await _unitOfWork.CommitAsync();

            return _return.SetSuccess(true);
        }

        public BlacklistDto GetList(BlacklistQueryDto dto)
        {
            if (dto == null) return null;
            dto.CheckPageInfo();
            var emails = _blackListRepository.GetAll();
            if (!string.IsNullOrEmpty(dto.Email))
            {
                emails = emails.Where(x => x.Email == dto.Email);
            }

            var r = new BlacklistDto();
            r.Total = emails.LongCount();
            emails = emails.OrderByDescending(x => x.CreateTime);
            emails = emails.Skip((dto.Pagination - 1) * dto.PageSize).Take(dto.PageSize);

            r.Emails = emails.AsEnumerable().Select(x => x.Email).ToList();

            return r;
        }

        public async Task<ReturnResult> RemoveBlackEmail(string email)
        {
            var emailDbData = _blackListRepository.GetEntityByEmail(email);
            if (emailDbData == null) return _return.SetMessage("该邮箱不在黑名单中");

            await _unitOfWork.Remove(emailDbData);
            await _unitOfWork.CommitAsync();
            return _return.SetSuccess(true);
        }

        /// <summary>
        /// 检查是否在黑名单内
        /// </summary>
        /// <param name="emails"></param>
        /// <returns>返回属于黑名单的邮箱，为空字符串则无任何黑名限制</returns>
        public string BlackEmailCheck(params string[] emails)
        {
            if (emails == null || emails.Length == 0)
                return "";
            List<string> blackEmails = new List<string>();
            foreach (var email in emails)
            {
                if (_blackListRepository.GetEntityByEmail(email) != null &&
                    !blackEmails.Contains(email))
                {
                    blackEmails.Add(email);
                }
            }
            return string.Join(";", blackEmails.ToArray());
        }
    }
}
