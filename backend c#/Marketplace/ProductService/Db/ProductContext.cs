using Microsoft.EntityFrameworkCore;
using ProductService.Entity;

namespace ProductService.Db
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Brands> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Subcategories> Subcategories { get; set; }
        public DbSet<BrandsSubcategories> BrandSubcategories { get; set; }
        public DbSet<ProductSizes> ProductSizes { get; set; }

        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
