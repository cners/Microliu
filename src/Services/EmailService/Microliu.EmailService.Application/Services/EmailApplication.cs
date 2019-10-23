using DotNetCore.CAP;
using Hangfire;
using Microliu.Core.Loggers;
using Microliu.Core.RedisCache;
using Microliu.EmailService.Application.Extensions;
using Microliu.EmailService.Application.IServices;
using Microliu.EmailService.Domain;
using Microliu.EmailService.Domain.Repositories;
using Microliu.EmailService.Domain.SeedWork;
using Microliu.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace Microliu.EmailService.Application.Services
{
    public partial class EmailApplication : IEmailService, ICapSubscribe
    {
        private readonly IServiceProvider _services;
        private ReturnResult _return;

        private readonly IEmailSendRepository _emailSendRepository;
        private readonly IUserInfoRepository _userInfoRepository;
        private readonly IProjectCategoryRepository _projectCategoryRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IBlackListRepository _blackListRepository;

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        private readonly IOptions<EmailServiceOptions> _emailSettings;
        private readonly ICacheService _cacheService;
        private readonly IBackgroundJobClient _jobs ;

        public EmailApplication(IServiceProvider services, IOptions<EmailServiceOptions> emailSettings)
        {
            _services = services;
            _logger = _services.GetService<ILogger>();
            _unitOfWork = _services.GetService<IUnitOfWork>();
            _cacheService = _services.GetService<ICacheService>();
            _jobs = services.GetService<IBackgroundJobClient>();

            _emailSendRepository = _services.GetService<IEmailSendRepository>();
            _userInfoRepository = _services.GetService<IUserInfoRepository>();
            _projectCategoryRepository = _services.GetService<IProjectCategoryRepository>();
            _projectRepository = _services.GetService<IProjectRepository>();
            _blackListRepository = _services.GetService<IBlackListRepository>();

            _emailSettings = emailSettings;

            _return = new ReturnResult();
            _return.SetSuccess(false);
        }
    }
}
