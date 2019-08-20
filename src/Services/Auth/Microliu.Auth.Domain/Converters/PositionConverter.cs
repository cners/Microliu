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
            qpm.IsEnable = position.IsEnable;
            qpm.Name = position.Name;
            qpm.Sort = position.Sort;
            qpm.CreateTime = position.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
            return qpm;
        }
    }
}
