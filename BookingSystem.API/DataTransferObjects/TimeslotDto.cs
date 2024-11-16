using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BookingSystem.Entities.Model;
using System.Runtime.Serialization;
using BookingSystem.API.Extensions;

namespace BookingSystem.API.DataTransferObjects;
public class CreateTimeSlotDto
{
    [Required]
    public Guid ProviderId { get; set; }

    [Required]
    public DateTime StartTime { get; set; }

    [Required]
    [EndTimeValidation]
    public DateTime EndTime { get; set; }
}

public class UpdateTimeSlotDto : CreateTimeSlotDto
{
    [Required]
    public string TimeSlotId { get; set; }
    [Required]
    [JsonConverter(typeof(JsonStringEnumConverter))]  
    public TimeSlot.TimeSlotStatus? Status { get; set; }
}
public class TimeslotDto
{
    [Key]
    public string TimeSlotId { get; set; } 
    [Required]
    public string ProviderId { get; set; }

    [Required]
    public DateTime StartTime { get; set; }

    [Required]
    public DateTime EndTime { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]  
    [Required]
    public TimeSlot.TimeSlotStatus Status { get; set; }
}