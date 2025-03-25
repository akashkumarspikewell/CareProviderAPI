using CareProviderAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CareProviderAPI.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions dbContextOptions):base(dbContextOptions) { }
        public DbSet<CareProvider> CareProviders { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<Experience> Experiences { get; set; }
    }
}
