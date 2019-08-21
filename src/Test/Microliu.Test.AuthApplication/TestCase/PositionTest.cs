using Microliu.Auth.Application;
using Microliu.Auth.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Microliu.Test.AuthApplication
{
    public class PositionTest
    {
        private IAuthApplication _authApplication;
        public PositionTest()
        {
            _authApplication = ApplicationFactory.GetIAuthApplication();
        }

        [Fact]
        public void CreatePosition()
        {
            var craetePosition = new CreatePositionModel
            {
                Name = Guid.NewGuid().ToString("N").Substring(1, 5),
                Sort = 100,
                IsEnable = 1
            };
            _authApplication.CreatePosition(craetePosition);
        }
    }
}
