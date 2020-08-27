using System.ComponentModel.DataAnnotations;

namespace DogsWeb.API.Dtos
{
    public class UserForRegister
    {
        [Required (ErrorMessage= "Username pole jest wymagane")]
        public string Username { get; set; }

        [Required (ErrorMessage= "Password pole jest wymagane")]
        [StringLength(20, MinimumLength = 4, ErrorMessage= "Musisz podać hasło o długości od 4 do 20 znaków")]
        public string Password { get; set; }
    }
}