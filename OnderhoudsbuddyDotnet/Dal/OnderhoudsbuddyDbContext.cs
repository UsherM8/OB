using Microsoft.EntityFrameworkCore;
using Dal.Entities;

namespace Dal
{
    public class OnderhoudsbuddyDbContext : DbContext
    {
        public OnderhoudsbuddyDbContext(DbContextOptions<OnderhoudsbuddyDbContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; } = null!;
        public DbSet<UserCar> UserCars { get; set; } = null!;
        public DbSet<Service> Services { get; set; } = null!;
        public DbSet<Garage> Garages { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Car>(entity =>
            {
                entity.HasKey(e => e.CarId);

                entity.Property(e => e.LicensePlate)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Mileage)
                    .IsRequired();
            });

            modelBuilder.Entity<UserCar>(entity =>
            {
                entity.HasKey(e => e.UserCarId);
                entity.Property(e => e.UserId)
                    .IsRequired();
                entity.Property(e => e.CarId)
                    .IsRequired();
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasKey(e => e.ServiceId);
                entity.Property(e => e.ServiceType)
                    .IsRequired();
                entity.Property(e => e.Status)
                    .IsRequired();
                entity.Property(e => e.Description)
                    .IsRequired();
                entity.Property(e => e.ServiceDate)
                    .IsRequired();
                entity.Property(e => e.NextServiceDate)
                    .IsRequired();
                entity.Property(e => e.CarId)
                    .IsRequired();
                entity.Property(e => e.GarageId)
                    .IsRequired();
            });

            modelBuilder.Entity<Garage>(entity =>
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
