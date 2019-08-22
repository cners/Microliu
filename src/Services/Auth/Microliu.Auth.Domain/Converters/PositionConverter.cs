using AutoMapper;
using Microliu.Auth.Domain.Entities;
using Microliu.Auth.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microliu.Auth.Domain.Converters
{
    public class PositionConverter
    {
        public static QueryPositionModel ToQueryPositionModel(Position position)
        {


            var qpm = new QueryPositionModel();
            qpm.Id = position.Id;
            qpm.IsEnable = (int)position.IsEnabled;
            qpm.Name = position.Name;
            qpm.Sort = position.Sort;
            qpm.CreateTime = position.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");

            return qpm;
        }

        public static Position ToPosition(CreatePositionModel input)
        {
            if (input == null)
                throw  new NullReferenceException();

            return new Position
            {
                Id = Guid.NewGuid().ToString("N"),
                CreateTime = DateTimeOffset.Now,
                Name =input.Name,
                IsDelete =  IsDelete.NotDelete,
                IsEnabled =  IsEnabled.Enabled,
                Sort = input.Sort
            };
        }
    }
}
