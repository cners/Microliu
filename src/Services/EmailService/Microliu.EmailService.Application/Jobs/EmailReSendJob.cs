using Hangfire;
using Microliu.Core.Logger;
using Microsoft.Extensions.Options;
using Pomelo.AspNetCore.TimedJob;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microliu.EmailService.Application.Jobs
{
    public class EmailReSendJob : Job
    {
        private readonly ILogger _logger;

        public EmailReSendJob(ILogger logger)
        {
            _logger = logger;
        }

        //[Invoke(Begin = "2019-09-06 10:27", Interval = 100, SkipWhileExecuting = true)]
        public void Run()
        {

        }
    }
}
