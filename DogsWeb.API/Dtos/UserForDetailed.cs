using System;
using System.Collections.Generic;
using DogsWeb.API.Models;

namespace DogsWeb.API.Dtos
{
    public class UserForDetailed
    {
       public string Id {get; set;}
       public string UserName {get; set;}
       public string Breed { get; set; } // Rasa
       public int  Age {get; set;}
       public string KnownAs { get; set; }
       public DateTime Created { get; set; }
       public DateTime LastActive { get; set; }
       public string Introduction { get; set; }
       public string LookingFor { get; set; }
       public string Interests { get; set; }
       public string City { get; set;}
       public string PhotoUrl { get; set; }
       public ICollection<Photo> Photos {get; set;}

    }
}