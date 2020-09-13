using System.Collections.Generic;
using System.Threading.Tasks;
using DogsWeb.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DogsWeb.API.Data
{
    public class DogsRepository : IDogsRepository
    {
        private readonly ApplicationDbContext _context;
        public DogsRepository(ApplicationDbContext context){

            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
           
           _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
             _context.Remove(entity);
        }

        public async Task<ApplicationUser> GetUser(string id)
        {
           var user = await _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.Id == id);

           return user;
        }

        public async Task<IEnumerable<ApplicationUser>> GetUsers()
        {
           var users = await _context.Users.Include(p => p.Photos).ToListAsync();

           return users;
        }

        public async Task<bool> SaveAll()
        {
           return await _context.SaveChangesAsync() > 0;
        }
    }
}