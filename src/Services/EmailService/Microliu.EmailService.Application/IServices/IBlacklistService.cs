using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microliu.EmailService.Application.ViewModel;
using Microliu.Utils;

namespace Microliu.EmailService.Application.IServices
{
    public interface IBlacklistService
    {
        Task<ReturnResult> CreateBlacklist(string email);

        Task<ReturnResult> RemoveBlackEmail(string email);

        BlacklistDto GetList(BlacklistQueryDto dto);
    }
}
