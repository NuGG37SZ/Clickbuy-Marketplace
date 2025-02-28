using Microsoft.EntityFrameworkCore;
using ProductService.Entity;

namespace ProductService.Db
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Subcategories> Subcategories { get; set; }

        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
