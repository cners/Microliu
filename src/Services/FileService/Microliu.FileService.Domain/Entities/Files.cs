using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Microliu.FileService.Domain
{
    [Table("Files")]
    public class Files
    {
        [Key]
        public string PathID { get; set; }

        public string StorageIP { get; set; }

        public string VisitIP { get; set; }

        public string Path { get; set; }

        public string Suffix { get; set; }

        public string Filename { get; set; }

        public string FileType { get; set; }

        public DateTime? UploadTime { get; set; }

        public string DevID { get; set; }
    }
}
