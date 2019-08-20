using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Microliu.Auth.Domain.Entities
{
    [Table("Position")]
    public class Position
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public int Sort { get; set; }

        public DateTimeOffset CreateTime { get; set; }

        public int IsEnable { get; set; }

        public int IsDelete { get; set; }

    }
}
