using System;
using System.Collections.Generic;
using System.Text;

namespace Microliu.EmailService.Application.ViewModel
{
    public class BlacklistQueryDto
    {
        public int PageSize { get; set; }

        public int Pagination { get; set; }

        public string Email { get; set; }

        public void CheckPageInfo()
        {
            this.PageSize = this.PageSize > 300 ? 300 : this.PageSize;
            this.PageSize=this.PageSize<0?0:this.PageSize;

            this.Pagination = this.Pagination > 1000 ? 1000 : this.Pagination;
            this.Pagination = this.Pagination < 1 ? 1 : this.Pagination;
        }
    }
}
