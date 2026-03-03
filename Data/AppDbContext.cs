using Microsoft.EntityFrameworkCore;
using wepAPI_denemeler.Models;

namespace wepAPI_denemeler.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
    }
}
