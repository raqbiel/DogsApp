using System.Threading.Tasks;
using DogsWeb.API.Models;

namespace DogsWeb.API.Data
{
    public interface IAuthRepository
    {
         Task<User> Register(User user, string password);
         Task<User> Login(string username, string password);
         Task<bool> UserExist(string username);

         Task<User> GetUserName(string username);

         Task<User> ResetPassword(User user, string password);

        
    }
}