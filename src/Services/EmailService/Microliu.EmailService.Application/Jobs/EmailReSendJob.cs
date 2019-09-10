using Hangfire;
using Microliu.Core.Loggers;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Microliu.EmailService.Application.Jobs
{
    public class EmailReSendJob
    {
        private readonly ILogger _logger;

        public EmailReSendJob(ILogger logger)
        {
            _logger = logger;
        }

        [AutomaticRetry(Attempts = 3)]
        [DisplayName("retry send email")]
        [Queue("jobs")]
        public void RetrySend()
        {
            _logger.Debug("重试发送邮件");
        }

    }
}
