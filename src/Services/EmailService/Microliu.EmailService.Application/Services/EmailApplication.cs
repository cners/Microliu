using DotNetCore.CAP;
using Hangfire;
using Microliu.Core.Loggers;
using Microliu.Core.RedisCache;
using Microliu.EmailService.Application.Extensions;
using Microliu.EmailService.Application.IServices;
using Microliu.EmailService.Domain.Repositories;
using Microliu.EmailService.Domain.SeedWork;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace Microliu.EmailService.Application.Services
{
    public partial class EmailApplication : IEmailService, ICapSubscribe
    {
        private readonly IServiceProvider _services;

        private readonly IEmailSendRepository _emailSendRepository;

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
            _emailSettings = emailSettings;
        }
    }
}
