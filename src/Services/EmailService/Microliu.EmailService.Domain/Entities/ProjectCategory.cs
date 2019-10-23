using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microliu.EmailService.Domain.Entities;
using Microliu.Utils;

namespace Microliu.EmailService.Domain
{
    [Table("PROJECT_CATEGORY")]
    public class ProjectCategory : BaseEntity
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        public long Uid { get; set; }

        public List<Project> Projects { get; set; }

        public List<RkUserProCategory> UserProCategories { get; set; }

        public ProjectCategory()
        {
            Id = SnowflakeId.Default().NextId();
            Name = "";
        }
    }
}
