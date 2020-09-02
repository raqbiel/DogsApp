using System.ComponentModel.DataAnnotations;

namespace DogsWeb.API.Dtos
{
    public class UserForRegister
    {
  
        [Required (ErrorMessage= "Nazwa użytkownika jest wymagana")]
        public string Username { get; set; }

        [Required (ErrorMessage= "Email pole jest wymagane")]
        [EmailAddress]
        public string Email { get; set; }

        [Required (ErrorMessage= "Hasło jest wymagane")]
        [StringLength(20, MinimumLength = 4, ErrorMessage= "Musisz podać hasło o długości od 4 do 20 znaków")]
        public string Password { get; set; }
        public string Firstname { get; set; }
    }
}