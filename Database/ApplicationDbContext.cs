using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(
            DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<ProductInfo> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductInfo>()
                .ToTable("PRODUCT_INFO");

           modelBuilder.Entity<ProductInfo>()
            .Property(p => p.Id)
            .HasColumnName("ID"); // or the exact DB column name

            modelBuilder.Entity<ProductInfo>()
                .Property(x => x.RollNo)
                .HasColumnName("ROLL_NO");

            modelBuilder.Entity<ProductInfo>()
                .Property(x => x.GSM)
                .HasColumnName("GSM");

            modelBuilder.Entity<ProductInfo>()
                .Property(x => x.Width)
                .HasColumnName("WIDTH");

            modelBuilder.Entity<ProductInfo>()
              .Property(p => p.Twist)
              .HasColumnName("TWIST");


            modelBuilder.Entity<ProductInfo>()
              .Property(p => p.PillingTest)
              .HasColumnName("PILLING_TEST");

           

            modelBuilder.Entity<ProductInfo>()
              .Property(p => p.ColorFastness)
              .HasColumnName("COLOR_FASTNESS");
               
        
    }
    }
}