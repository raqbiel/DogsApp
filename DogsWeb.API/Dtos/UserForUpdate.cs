using System;

namespace DogsWeb.API.Dtos
{
    public class UserForUpdate
    {
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }               

    }
}