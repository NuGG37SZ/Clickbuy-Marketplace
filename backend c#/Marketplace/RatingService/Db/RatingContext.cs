using Microsoft.EntityFrameworkCore;
using RatingService.Entity;

namespace RatingService.Db
{
    public class RatingContext : DbContext
    {
        public DbSet<RatingProduct> RatingProducts { get; set; }

        public DbSet<RatingSeller> RatingSellers { get; set; }

        public RatingContext(DbContextOptions<RatingContext> options) : base(options) { }
    }
}
