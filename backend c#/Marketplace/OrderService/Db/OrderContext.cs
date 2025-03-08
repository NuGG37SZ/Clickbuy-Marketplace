using Microsoft.EntityFrameworkCore;
using OrderService.Entity;

namespace OrderService.Db
{
    public class OrderContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderProduct> OrderProducts { get; set; }

        public OrderContext(DbContextOptions<OrderContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }
    }
}
