using System.ComponentModel.DataAnnotations;

namespace DogsWeb.API.Models
{
    public class ForgotPasswordModel
    {
        [Required (ErrorMessage= "Email pole jest wymagane")]
        [EmailAddress]
        public string Email {get; set;}
    }
}