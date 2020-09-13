using System;

namespace DogsWeb.API.Dtos
{
    public class UserForList
    {
       public string Id {get; set;}
       public string UserName {get; set;}
       public int EmailConfirmed {get;set;}
       public string Breed { get; set; } // Rasa
       public int Age {get; set;}
       public string KnownAs { get; set; }
       public DateTime Created { get; set; }
       public DateTime LastActive { get; set; }
       public string City { get; set;}
       public string PhotoUrl {get; set;}

    }
}