using DogsWeb.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DogsWeb.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){ }

        public DbSet<Value> Values { get; set; }
    }
}