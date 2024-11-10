using BookingSystem.Entities.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingSystem.Entities.EntityConfigurations
{
    
    public class ProviderConfiguration : IEntityTypeConfiguration<Provider>
    {
        public void Configure(EntityTypeBuilder<Provider> builder)
        {
            builder
                .HasMany(p => p.Services)
                .WithOne(s => s.Provider)
                .HasForeignKey(s => s.ProviderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(p => p.TimeSlots)
                .WithOne(t => t.Provider)
                .HasForeignKey(t => t.ProviderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(p => p.Bookings)
                .WithOne(b => b.Provider)
                .HasForeignKey(b => b.ProviderId)
                .OnDelete(DeleteBehavior.Restrict);
            
        }
    }

    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.HasKey(s => s.ServiceId);
        }
    }
    
    public class BookingConfiguration : IEntityTypeConfiguration<BookingSystem.Entities.Model.Booking>
    {
        public void Configure(EntityTypeBuilder<BookingSystem.Entities.Model.Booking> builder)
        {
            builder.HasKey(b => b.BookingId);
            
            builder.HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b.Provider)
                .WithMany(p => p.Bookings)
                .HasForeignKey(b => b.ProviderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b.TimeSlot)
                .WithOne(ts => ts.Booking)
                .HasForeignKey<BookingSystem.Entities.Model.Booking>(b => b.TimeSlotId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}