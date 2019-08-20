using System;
using System.Collections.Generic;
using System.Text;

namespace Microliu.Auth.Domain.ViewModels
{
    public class QueryPositionModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int Sort { get; set; }

        public string CreateTime { get; set; }

        public int IsEnable { get; set; }
    }
}
