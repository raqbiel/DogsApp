using System.ComponentModel.DataAnnotations;

namespace DogsWeb.API.Models
{
    public class ResetPasswordModel
    {
       
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name= "Potwierdź hasło")]
        [Compare("Password", ErrorMessage = "Hasło i hasło muszą się zgadzać")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string token {set; get;}
    }
}