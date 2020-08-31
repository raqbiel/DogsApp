using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DogsWeb.API.Models
{
    public class ApplicationUser: IdentityUser
    {

        //custom fields to Identity
       public string FirstName { get; set; }
       public string LastName { get; set; }
    }
}