using Microsoft.EntityFrameworkCore;
using Domain.Dtos;

namespace Dal
{
    public class OnderhoudsbuddyDbContext : DbContext
    {
        public OnderhoudsbuddyDbContext(DbContextOptions<OnderhoudsbuddyDbContext> options) 
            : base(options)
        {
        }

        public DbSet<CarDto> Cars { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CarDto>(entity =>
            {
                entity.HasKey(e => e.CarId);

                entity.Property(e => e.LicencePlate)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.BrandName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CarType)
                    .IsRequired()
                    .HasMaxLength(100);
            });
        }
    }
}