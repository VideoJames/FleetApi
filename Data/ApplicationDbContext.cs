using Microsoft.EntityFrameworkCore;
using WorkflowApi.Models;

namespace WorkflowApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<DeviceReading> DeviceReadings => Set<DeviceReading>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DeviceReading>(entity =>
            {
                // Precision
                entity.Property(e => e.BatteryVoltage)
                    .HasPrecision(5, 2);

                entity.Property(e => e.Temperature)
                    .HasPrecision(5, 2);

                // Relationship
                entity.HasOne(e => e.User)
                    .WithMany(u => u.DeviceReadings)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.SetNull);

                // Indexes
                entity.HasIndex(e => e.UserId)
                    .HasDatabaseName("IX_DeviceReadings_UserId");

                entity.HasIndex(e => e.DeviceName)
                    .HasDatabaseName("IX_DeviceReadings_DeviceName");

                entity.HasIndex(e => e.RecordedAt)
                    .HasDatabaseName("IX_DeviceReadings_RecordedAt");
            });
        }
    }
}
