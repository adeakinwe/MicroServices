using System;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> option) : base(option)
        {
            
        }

        public DbSet<TBL_CUSTOMER> TBL_CUSTOMER { get; set; }
        public DbSet<TBL_CUSTOMER> TBL_CUSTOMER_ORDER { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TBL_CUSTOMER>().ToTable("TBL_CUSTOMER");
            modelBuilder.Entity<TBL_CUSTOMER_ORDER>().ToTable("TBL_CUSTOMER_ORDER");

            modelBuilder.Entity<TBL_CUSTOMER>()
            .HasMany(c => c.ORDERS)
            .WithOne(o => o.CUSTOMER)
            .HasForeignKey(o => o.CUSTOMERID);
        }
    }
}