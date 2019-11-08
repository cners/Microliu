using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Microliu.EmailService.Application.ViewModel
{
    public class EmailLogQueryDto : SkipPageTemplateDto
    {
        //[Required(AllowEmptyStrings =false, ErrorMessage = "请填写项目标识")]
        public string ProjectId { get; set; }
    }
}
