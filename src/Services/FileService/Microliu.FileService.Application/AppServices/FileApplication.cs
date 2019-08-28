using Microliu.FileService.Data;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Text;

namespace Microliu.FileService.Application
{
    public partial class FileApplication : IFileApplication
    {
        private readonly IServiceProvider _services;
        private IUnitOfWork _unitOfWork;

        public FileApplication(IServiceProvider services)
        {
            _services = services;

            _unitOfWork = services.GetService<IUnitOfWork>();
        }

    }
}
