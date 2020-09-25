using System.Collections.Generic;
using System.Threading.Tasks;
using DogsWeb.API.Models;

namespace DogsWeb.API.Data
{
    public interface IDogsRepository
    {
         void Add<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         Task<bool> SaveAll();
         Task<IEnumerable<ApplicationUser>> GetUsers();
         Task<ApplicationUser> GetUser(string id);
         Task<Photo> GetPhoto(int id);
    }
}