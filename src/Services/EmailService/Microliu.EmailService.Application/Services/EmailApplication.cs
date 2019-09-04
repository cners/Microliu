using DotNetCore.CAP;
using Microliu.EmailService.Application.IServices;
using Microliu.EmailService.Domain.Repositories;
using Microliu.EmailService.Domain.SeedWork;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Text;
using Microliu.Core.Logger;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microliu.EmailService.Application.Extensions;

namespace Microliu.EmailService.Application.Services
{
    public partial class EmailApplication : IEmailService, ICapSubscribe
    {
        private readonly IServiceProvider _services;

        private readonly IEmailSendRepository _emailSendRepository;

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        private readonly IOptions<EmailServiceSettings> _emailSettings;

        public EmailApplication(IServiceProvider services, IOptions<EmailServiceSettings> emailSettings)
        {
            _services = services;
            _logger = _services.GetService<ILogger>();
            _unitOfWork = _services.GetService<IUnitOfWork>();

            _emailSendRepository = _services.GetService<IEmailSendRepository>();
            _emailSettings = emailSettings;


        }

    }
}
