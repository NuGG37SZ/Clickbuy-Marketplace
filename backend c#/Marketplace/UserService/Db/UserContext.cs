using Microsoft.EntityFrameworkCore;
using UserService.Entity;

namespace UserService.Db
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UserContext(DbContextOptions<UserContext> options) : base(options)
        { 
            Database.EnsureCreated();
        }
    }
}
