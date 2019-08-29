using Microliu.Auth.Domain;
using Microliu.Auth.Domain.Repositories;
using Microliu.Auth.Domain.SeedWork;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microliu.Auth.Application
{
    public partial class AuthApplication : IAuthService
    {
        private readonly IServiceProvider _services;

        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPositionRepository _positionRepository;

        private readonly IUnitOfWork _unitOfWork;
        public AuthApplication(IServiceProvider services)
        {
            _services = services;
            _unitOfWork = services.GetService<IUnitOfWork>();

            _roleRepository = services.GetService<IRoleRepository>();
            _userRepository = services.GetService<IUserRepository>();
            _positionRepository = services.GetService<IPositionRepository>();
        }


    }
}
