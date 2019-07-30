using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace News.IApplication.Dtos
{
    public abstract class NewsDtoBase
    {
        [Required(ErrorMessage = "标题不能为空")]
        public string Title { get; set; }

        [Required(ErrorMessage = "内容不能为空")]
        public string Content { get; set; }
    }
}
