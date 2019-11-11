using System;
using System.Collections.Generic;
using System.Text;

namespace Microliu.EmailService.Application
{
    public class SkipPageTemplateDto
    {
        public int PageSize { get; set; }

        public int Pagination { get; set; }

        public SkipPageTemplateDto()
        {
            this.PageSize = this.PageSize > 3000 ? 3000 : this.PageSize;
            this.PageSize = this.PageSize < 0 ? 0 : this.PageSize;

            this.Pagination = this.Pagination > 100000 ? 100000 : this.Pagination;
            this.Pagination = this.Pagination < 1 ? 1 : this.Pagination;
        }
    }
}
