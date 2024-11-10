using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSystem.Entities.Model;
public class Booking : BaseEntity
{
    [Key]
    public string BookingId { get; set; } = Guid.NewGuid().ToString();
    
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public string ProviderId { get; set; }

    [Required]
    public string ServiceId { get; set; }

    [Required]
    public string TimeSlotId { get; set; }

    [Required]
    public BookingStatus Status { get; set; }

    // Navigation properties
    [ForeignKey("UserId")]
    public ApplicationUser User { get; set; }

    [ForeignKey("ProviderId")]
    public Provider Provider { get; set; }

    [ForeignKey("ServiceId")]
    public Service Service { get; set; }

    [ForeignKey("TimeSlotId")]
    public TimeSlot TimeSlot { get; set; }
    
    public enum BookingStatus
    {
        Available,
        Booked,
        Blocked,
        Cancelled
    }
}