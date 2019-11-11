using Microliu.EmailService.Application.IServices;
using System;
using Xunit;

namespace Microliu.EmailService.Application.Test.TestCase
{
    public class RedisServiceTest
    {
        private readonly IEmailService _emailService;

        public RedisServiceTest()
        {
            _emailService = ApplicationFactory.GetIEmailApplication();
        }

        
    }
}
