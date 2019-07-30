using Surging.Core.Domain.PagedAndSorted;
using System;
using System.Collections.Generic;
using System.Text;

namespace News.IApplication.Dtos
{
   public class QueryNewsInput:PagedResultRequestDto
    {
        public string SearchKey { get; set; }

        public string SearchTitle { get; set; }
    }
}
