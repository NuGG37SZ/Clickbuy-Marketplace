using Microsoft.EntityFrameworkCore;
using ProductService.Entity;

namespace ProductService.Db
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ProductContext()
        {
            Database.EnsureCreated();
        }
    }
}
