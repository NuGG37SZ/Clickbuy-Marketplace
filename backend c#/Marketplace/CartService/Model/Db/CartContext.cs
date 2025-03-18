using CartService.Model.Entity;
using Microsoft.EntityFrameworkCore;

namespace CartService.Model.Db
{
    public class CartContext : DbContext
    {
        public DbSet<Cart> Carts { get; set; }

        public DbSet<Favorites> Favorites { get; set; }

        public CartContext(DbContextOptions<CartContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
