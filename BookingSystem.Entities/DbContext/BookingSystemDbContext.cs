using BookingSystem.Entities.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace BookingSystem.Entities.DbContext
{
    public class BookingSystemDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>

    {
        public BookingSystemDbContext(DbContextOptions<BookingSystemDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Provider> Providers { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            modelBuilder.Entity<IdentityRole<Guid>>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("UserToken");
            base.OnModelCreating(modelBuilder);

            // Provider relationships
            modelBuilder.Entity<Provider>()
                .HasMany(p => p.Services)
                .WithOne(s => s.Provider)
                .HasForeignKey(s => s.ProviderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Provider>()
                .HasMany(p => p.TimeSlots)
                .WithOne(t => t.Provider)
                .HasForeignKey(t => t.ProviderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Provider>()
                .HasMany(p => p.Bookings)
                .WithOne(b => b.Provider)
                .HasForeignKey(b => b.ProviderId)
                .OnDelete(DeleteBehavior.Restrict);

            // TimeSlot relationships
            modelBuilder.Entity<TimeSlot>()
                .HasOne(t => t.Booking)
                .WithOne(b => b.TimeSlot)
                .HasForeignKey<Booking>(b => b.TimeSlotId)
                .OnDelete(DeleteBehavior.Restrict);

            // User relationships
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Bookings)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Service relationships
            modelBuilder.Entity<Service>()
                .HasMany(s => s.Bookings)
                .WithOne(b => b.Service)
                .HasForeignKey(b => b.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            modelBuilder.Entity<Provider>()
                .HasIndex(p => p.Email)
                .IsUnique();

            modelBuilder.Entity<ApplicationUser>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<TimeSlot>()
                .HasIndex(t => new { t.ProviderId, t.StartTime, t.EndTime });
            
            modelBuilder.Entity<TimeSlot>().Property(e => e.Status).HasConversion(
                v => v.ToString(),
                v => (TimeSlot.TimeSlotStatus)Enum.Parse(typeof(TimeSlot.TimeSlotStatus), v));
            modelBuilder.Entity<Provider>().Property(e => e.Status).HasConversion(
                v => v.ToString(),
                v => (Provider.ProviderStatus)Enum.Parse(typeof(Provider.ProviderStatus), v));
            modelBuilder.Entity<Service>().Property(e => e.Status).HasConversion(
                v => v.ToString(),
                v => (Service.ServiceStatus)Enum.Parse(typeof(Service.ServiceStatus), v));
            modelBuilder.Entity<Booking>().Property(e => e.Status).HasConversion(
                v => v.ToString(),
                v => (Booking.BookingStatus)Enum.Parse(typeof(Booking.BookingStatus), v));
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}