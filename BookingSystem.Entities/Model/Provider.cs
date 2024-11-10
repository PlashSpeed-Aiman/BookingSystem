using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Entities.Model;

public class Provider : BaseEntity
{
    [Key]
    public string? ProviderId { get; set; } = Guid.NewGuid().ToString();

    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public ProviderStatus Status { get; set; }

    // Navigation properties
    public ICollection<Service> Services { get; set; }
    public ICollection<TimeSlot> TimeSlots { get; set; }
    public ICollection<Booking> Bookings { get; set; }

    public enum ProviderStatus
    {
        Active,
        Inactive
    }
}