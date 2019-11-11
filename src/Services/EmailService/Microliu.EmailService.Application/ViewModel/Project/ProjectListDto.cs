using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Microliu.EmailService.Application
{
    public class ProjectDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CreateTime { get; set; }
        public string CategoryName { get; set; }
    }
    public class ProjectListDto
    {
        public List<ProjectDto> Projects { get; set; }
        public int Total { get; set; }
    }

    public class ProjectQueryDto : SkipPageTemplateDto
    {
        public string ProjectName { get; set; }

        //[Required]
        public long UserId { get; set; }
    }
}
