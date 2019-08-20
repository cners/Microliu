using Microliu.Auth.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microliu.Auth.Application
{
    public partial class AuthApplication
    {
        public async Task CreatePosition(CancellationToken ct = default)
        {
            await _unitOfWork.RegisterNew<Position>(new Position
            {
                Id = Guid.NewGuid().ToString("N"),
                CreateTime = DateTimeOffset.Now,
                Name = ".NET工程师",
                IsDelete = 1,
                IsEnable = 1,
                Sort = 100
            });

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
