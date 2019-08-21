using Microliu.Auth.Domain;
using Microliu.Auth.Domain.Converters;
using Microliu.Auth.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microliu.Auth.Application
{
    public partial class AuthApplication
    {
        public async Task CreatePosition(CreatePositionModel input, CancellationToken ct = default)
        {
            var createPosition = PositionConverter.ToPosition(input);
            await _unitOfWork.RegisterNew<Position>(createPosition);

            await _unitOfWork.CommitAsync();
        }

        public async Task SetUserPosition(string userId, string positionId, CancellationToken ct = default)
        {
            await _unitOfWork.RegisterNew<UserPosition>(new UserPosition
            {
                Id = Guid.NewGuid().ToString("N"),
                UserId = userId,
                PositionId = positionId,
                CreateTime = DateTimeOffset.Now
            });

            await _unitOfWork.CommitAsync();
        }

        public dynamic GetUsers(string positionId)
        {
            return _userRepos.Query("", positionId);
        }

        public dynamic GetPosition(string id)
        {
            return _position.Get(id);
        }
    }
}
