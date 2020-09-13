using System;
using System.Collections.Generic;
using System.Linq;
using DogsWeb.API.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace DogsWeb.API.Data
{
    public class Seed
    {
     
        public static void SeedUsers(ApplicationDbContext context, UserManager<ApplicationUser> userMgr)
        {
         

            if (!context.Users.Any())
            {

                var userData = System.IO.File.ReadAllText("Data/DataSeed.json");
                var users = JsonConvert.DeserializeObject<List<ApplicationUser>>(userData);
              

                foreach (var user in users)
                {          
                     userMgr.AddPasswordAsync(user, "19Beata66!");
                     user.UserName = user.UserName.ToLower();
                     context.Users.Add(user);

                }

                context.SaveChanges();
            }

        }
    }
}