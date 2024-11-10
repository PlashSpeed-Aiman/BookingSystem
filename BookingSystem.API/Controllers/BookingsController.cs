using BookingSystem.API.DataTransferObjects;
using BookingSystem.Entities.DbContext;
using BookingSystem.Entities.Model;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingsController : ControllerBase
{
    private readonly ILogger<BookingsController> _logger;
    private readonly BookingSystemDbContext _context;

    public BookingsController(BookingSystemDbContext context, ILogger<BookingsController> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    // POST   /api/v1/bookings
    [HttpPost]
    public async Task<IActionResult> CreateBooking([FromBody] CreateBookingDto createBookingDto)
    {
        var res = createBookingDto.Adapt<Booking>();
        res.Status = Booking.BookingStatus.Booked;
        var booking = _context.Bookings.Add(res);
        
        var timeslot = await _context.TimeSlots.FindAsync(createBookingDto.TimeSlotId);
        timeslot.Status = TimeSlot.TimeSlotStatus.Booked;
        await _context.SaveChangesAsync();
        
        var bookingDto = booking.Entity.Adapt<BookingDto>();
        return CreatedAtAction(nameof(GetBooking), new { id = bookingDto.BookingId }, bookingDto);
    }

    // GET    /api/v1/bookings/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBooking(string id)
    {
        var booking = await _context.Bookings.FindAsync(id);
        if (booking == null)
        {
            return NotFound();
        }
        var bookingDto = booking.Adapt<BookingDto>();
        return Ok(bookingDto);
    }

    // PATCH  /api/v1/bookings/{id}/cancel
    [HttpPatch("{id}/cancel")]
    public async Task<IActionResult> CancelBooking(string id)
    {
        var booking = await _context.Bookings.FindAsync(id);
        if (booking == null)
        {
            return NotFound();
        }
        booking.Status = Booking.BookingStatus.Cancelled;
        
        var timeslot = await _context.TimeSlots.FindAsync(booking.TimeSlotId);
        timeslot.Status = TimeSlot.TimeSlotStatus.Available;
        
        await _context.SaveChangesAsync();
        var bookingDto = booking.Adapt<BookingDto>();
        return Ok(bookingDto);
    }

    // GET
    // /api/v1/bookings/user/{user_id}
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUserBookings([FromRoute] string userId)
    {
        var bookings = await _context.Bookings.Where(b => b.UserId == Guid.Parse(userId)).ToListAsync();
        var bookingsDto = bookings.Select(x => x.Adapt<BookingDto>()).ToList();
        return Ok(bookingsDto);
    }

    // GET
    // /api/v1/bookings/provider/{provider_id}
    [HttpGet("provider/{providerId}")]
    public async Task<IActionResult> GetProviderBookings([FromRoute] string providerId)
    {
        var bookings = await _context.Bookings.Where(b => b.ProviderId == providerId).ToListAsync();
        var bookingsDto = bookings.Select(x => x.Adapt<BookingDto>()).ToList();

        return Ok(bookingsDto);
    }
}