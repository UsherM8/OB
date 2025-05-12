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

                entity.Property(e => e.LicensePlate)
                    .IsRequired()
                    .HasMaxLength(50);
                
                entity.Property(e => e.Mileage)
                    .IsRequired()
                    .HasMaxLength(50);
                
                // Uitsluiten van overige eigenschappen
                entity.Ignore(e => e.Brand);
                entity.Ignore(e => e.TradeName);
                entity.Ignore(e => e.VehicleType);
                entity.Ignore(e => e.PrimaryColor);
                entity.Ignore(e => e.ApkExpiryDate);
                entity.Ignore(e => e.EmptyVehicleMass);
                entity.Ignore(e => e.RegistrationDate);
                entity.Ignore(e => e.FirstAdmissionDate);
                entity.Ignore(e => e.MileageJudgment);
            });
        }
    }
}