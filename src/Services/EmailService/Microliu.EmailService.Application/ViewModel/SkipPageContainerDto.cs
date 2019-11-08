using System;
using System.Collections.Generic;
using System.Text;

namespace Microliu.EmailService.Application.ViewModel
{
    /// <summary>
    /// 分页数据返回传输对象
    /// </summary>
    public class SkipPageContainerDto
    {
        public long Total { get; set; }

        public int Pagination { get; set; }

        public int PageSize { get; set; }

        public object Rows { get; set; }

        public static SkipPageContainerDto Set(int pagination,int pageSize,object rows,long total = 0)
        {
            SkipPageContainerDto dto = new SkipPageContainerDto();
            dto.Total = total;
            dto.PageSize = pageSize;
            dto.Pagination = pagination;
            dto.Rows = rows;
            return dto;
        }
    }
}
