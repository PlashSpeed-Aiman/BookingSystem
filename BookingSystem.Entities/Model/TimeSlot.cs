using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSystem.Entities.Model
{
    public class TimeSlot : BaseEntity
    {
        [Key]
        public string TimeSlotId { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string ProviderId { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public TimeSlotStatus Status { get; set; }

        // Navigation properties
        [ForeignKey("ProviderId")]
        public Provider Provider { get; set; }
        public Booking Booking { get; set; }

        public enum TimeSlotStatus
        {
            Available,
            Booked,
            Blocked,
            Cancelled,
        }
    }
}