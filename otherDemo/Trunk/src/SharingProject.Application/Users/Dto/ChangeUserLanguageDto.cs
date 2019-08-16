using System.ComponentModel.DataAnnotations;

namespace SharingProject.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}