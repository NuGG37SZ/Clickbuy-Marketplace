using Microsoft.EntityFrameworkCore;
using OrderService.Model.Entity;

namespace OrderService.Model.Db
{
    public class OrderContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Points> Points { get; set; }
        public DbSet<UserPoints> UserPoints { get; set; }

        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
