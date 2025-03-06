using Microsoft.EntityFrameworkCore;

namespace EventService.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> option) : base(option)
        {
            
        }

        public DbSet<TBL_EVENT> TBL_EVENT { get; set; }
        public DbSet<TBL_CUSTOMER> TBL_CUSTOMER { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TBL_EVENT>().ToTable("TBL_EVENT");
            modelBuilder.Entity<TBL_CUSTOMER>().ToTable("TBL_CUSTOMER");

            modelBuilder.Entity<TBL_CUSTOMER>()
            .HasMany(c => c.EVENTS)
            .WithOne(o => o.CUSTOMER!)
            .HasForeignKey(o => o.CUSTOMERID);

            modelBuilder.Entity<TBL_EVENT>()                        
            .HasOne(e => e.CUSTOMER)
            .WithMany(e => e.EVENTS)
            .HasForeignKey(e => e.CUSTOMERID);
        }
    }
}