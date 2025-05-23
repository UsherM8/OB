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
        public DbSet<UserCarDto> UserCars { get; set; } = null!;
        public DbSet<ServiceDto> Services { get; set; } = null!;
        public DbSet<GarageDto> Garages { get; set; } = null!;

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

            modelBuilder.Entity<UserCarDto>(entity =>
            {
                entity.HasKey(e => e.UserCarId);
                entity.Property(e => e.UserId)
                    .IsRequired();
                entity.Property(e => e.CarId)
                    .IsRequired();
            });

            modelBuilder.Entity<ServiceDto>(entity =>
            {
                entity.HasKey(e => e.ServiceId);
                entity.Property(e => e.ServiceType)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500);
                entity.Property(e => e.ServiceDate)
                    .IsRequired();
                entity.Property(e => e.NextServiceDate)
                    .IsRequired();
                entity.Property(e => e.CarId)
                    .IsRequired();
                entity.Property(e => e.GarageId)
                    .IsRequired();
            });

            modelBuilder.Entity<GarageDto>(entity =>
            {
                entity.HasKey(e => e.GarageId);
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.StreetName)
                    .IsRequired()
                    .HasMaxLength(200);
                entity.Property(e => e.HouseNumber)
                    .IsRequired()
                    .HasMaxLength(10);
                entity.Property(e => e.ExtraAddressInfo)
                    .HasMaxLength(100);
                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasMaxLength(10);
                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(15);
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);
            });
        }
    }
}