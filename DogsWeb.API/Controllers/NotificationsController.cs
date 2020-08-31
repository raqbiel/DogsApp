using Microsoft.AspNetCore.Mvc;

namespace DogsWeb.API.Controllers
{
   public class NotificationsController : Controller
    {
        public IActionResult EmailConfirmed(string userId, string code) 
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(code))
            {
                return Redirect("http://localhost:4200");

            }


            return View();
         }
    }
}