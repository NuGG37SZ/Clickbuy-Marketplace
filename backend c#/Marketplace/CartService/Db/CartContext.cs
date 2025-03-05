using CartService.Entity;
using Microsoft.EntityFrameworkCore;

namespace CartService.Db
{
    public class CartContext : DbContext
    {
        public DbSet<Cart> Carts { get; set; }

        public CartContext(DbContextOptions<CartContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
