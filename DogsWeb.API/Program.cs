using System;
using DogsWeb.API.Data;
using DogsWeb.API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DogsWeb.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
          
           var host = CreateHostBuilder(args).Build();
           using(var scope = host.Services.CreateScope())
           {
               
               var services = scope.ServiceProvider;
               try{
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    var _userManager = services.GetService<UserManager<ApplicationUser>>();
                    context.Database.Migrate();
                    Seed.SeedUsers(context, _userManager);
               }
               catch(Exception ex){
                   var logger = services.GetRequiredService<ILogger<Program>>();
                   logger.LogError(ex, "Błąd podczas migracji");
               }
           }
           host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
