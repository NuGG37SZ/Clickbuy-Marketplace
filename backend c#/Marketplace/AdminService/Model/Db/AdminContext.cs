using AdminService.Model.Entity;
using Microsoft.EntityFrameworkCore;

namespace AdminService.Model.Db
{
    public class AdminContext : DbContext
    {
        public DbSet<CategoryReport> CategoryReports { get; set; }

        public DbSet<Report> Reports { get; set; }

        public AdminContext(DbContextOptions<AdminContext> dbContextOptions) : base(dbContextOptions) 
        {
            Database.EnsureCreated();
        }
    }
}
