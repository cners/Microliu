using Microliu.Auth.Domain;
using Microliu.Auth.Domain.Converters;
using Microliu.Auth.Domain.Entities;
using Microliu.Auth.Domain.ViewModels;
using System;
using System.Linq;
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
            return _position.GetEntity(id);
        }

        public dynamic GetPositions()
        {
            return _position.GetAll().AsEnumerable().Select(e =>
            {
                return new
                {
                    Id = e.Id,
                    Name = e.Name,
                    IsEnable = e.IsEnabled,
                    Sort = e.Sort,
                    CreateTime = e.CreateTime.ToString("yyyy-MM-dd HH:mm:ss")
                };
            }).AsQueryable();
        }


        public dynamic GetPositions(SearchPositionModel input, CancellationToken ct = default)
        {
            return _position.GetPositions(input);
        }
    }
}
