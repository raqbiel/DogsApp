using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DogsWeb.API.Models
{
    public class ApplicationUser: IdentityUser
    {

       
        //custom fields to Identity
       public string Breed { get; set; } // Rasa
       public DateTime DateOfBirth {get; set;}
       public string KnownAs { get; set; }
       public DateTime Created { get; set; }
       public DateTime LastActive { get; set; }
       public string Introduction { get; set; }
       public string LookingFor { get; set; }
       public string Interests { get; set; }
       public string City { get; set;}
       public ICollection<Photo> Photos {get; set;}
    }
}