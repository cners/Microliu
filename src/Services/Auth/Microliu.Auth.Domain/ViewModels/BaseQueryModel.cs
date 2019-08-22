using System;
using System.Collections.Generic;
using System.Text;

namespace Microliu.Auth.Domain
{
    public class BaseByPageQueryModel
    {
        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 页码（从1开始）
        /// </summary>
        public int PageIndex { get; set; }

        public BaseByPageQueryModel()
        {
            PageIndex = 1;
            PageSize = 5;
        }

        public int GetSkipValue()
        {
            int skipValue =  (PageIndex - 1) * PageSize;
            return skipValue < 0 ? 0 : skipValue;
        }
    }
}
