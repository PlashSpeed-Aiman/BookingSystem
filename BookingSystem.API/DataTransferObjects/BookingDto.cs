using System.ComponentModel.DataAnnotations;
using BookingSystem.Entities.Model;

namespace BookingSystem.API.DataTransferObjects;

public class CreateBookingDto
{
    public Guid UserId { get; set; }

    public string ProviderId { get; set; }

    public string ServiceId { get; set; }

    public string TimeSlotId { get; set; }
}

public class UpdateBookingDto
{
    public string BookingId { get; set; } 

    public Guid UserId { get; set; }

    public string ProviderId { get; set; }

    public string ServiceId { get; set; }

    public string TimeSlotId { get; set; }
}
public class UpdateBookingStatusDto
{
    [Required]
    public Booking.BookingStatus Status { get; set; }
}

public class BookingDto
{
    public string BookingId { get; set; } 

    public Guid UserId { get; set; }

    public string ProviderId { get; set; }

    public string ServiceId { get; set; }

    public string TimeSlotId { get; set; }
    
    public Booking.BookingStatus Status { get; set; }
}