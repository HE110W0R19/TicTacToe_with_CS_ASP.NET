using System.ComponentModel.DataAnnotations;

namespace FirstWebApp.Models
{
    public class LoginDataModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Пж,введи норм ник,по хорошему :3 ")]
        public string? playerName { get; set; }
    }
}