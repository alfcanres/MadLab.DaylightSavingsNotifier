using DSTN.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace DSTN.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ObservedTimeZone> TimeZones { get; set; }
        public DbSet<Notification> Notifications { get; set; }

    }
}
