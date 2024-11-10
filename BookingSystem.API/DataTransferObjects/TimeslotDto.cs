using System.ComponentModel.DataAnnotations;
using BookingSystem.Entities.Model;

namespace BookingSystem.API.DataTransferObjects;
public class CreateTimeSlotDto
{
    [Required]
    public Guid ProviderId { get; set; }

    [Required]
    public DateTime StartTime { get; set; }

    [Required]
    public DateTime EndTime { get; set; }
}

public class UpdateTimeSlotDto
{
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public TimeSlot.TimeSlotStatus? Status { get; set; }
}
public class TimeslotDto
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
    public TimeSlot.TimeSlotStatus Status { get; set; }
}