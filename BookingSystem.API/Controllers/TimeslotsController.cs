using BookingSystem.API.DataTransferObjects;
using BookingSystem.Entities.DbContext;
using BookingSystem.Entities.Model;
using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.API.Controllers;

[Route("/api/[controller]")]
[ApiController]
public class TimeslotsController : ControllerBase
{
    private readonly BookingSystemDbContext _context;

    public TimeslotsController(BookingSystemDbContext context)
    {
        _context = context;
    }
    
    //GET    /api/v1/timeslots
    [HttpGet]
    public async Task<IActionResult> GetTimeslots()
    {
        var timeslots = await _context.TimeSlots.ToListAsync();
        return Ok(timeslots.Select(x => x.Adapt<TimeslotDto>()));
    }
    
    // GET    /api/v1/timeslots/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTimeslot(string id)
    {
        var timeslot = await _context.TimeSlots.FindAsync(id);
        if (timeslot == null)
        {
            return NotFound();
        }
        return Ok(timeslot.Adapt<TimeslotDto>());
    }
    
    //GET    /api/v1/timeslots/available
    // Query params: 
    // - date (YYYY-MM-DD)
    // - provider_id (optional)
    //     - service_id (optional)
    [HttpGet("available")]
    public async Task<IActionResult> GetAvailableTimeslots([FromQuery] string date, [FromQuery] string? providerId, [FromQuery] string? serviceId)
    {
        var query = _context.TimeSlots
            .Where(t => t.StartTime.Date.ToString("yyyy-MM-dd") == date && t.Status != TimeSlot.TimeSlotStatus.Booked);
        
        if (providerId != null)
        {
            query = query.Where(t => t.ProviderId == providerId);
        }
        
        // if (serviceId != null)
        // {
        //     query = query.Where(t => t. == serviceId);
        // }
        
        var timeslots = await query.ToListAsync();
        return Ok(timeslots.Select(x => x.Adapt<TimeslotDto>()));
    }
    //POST    /api/v1/timeslots
    [HttpPost]
    public async Task<IActionResult> CreateTimeslot([FromBody] CreateTimeSlotDto createTimeslotDto)
    {
        var timeslot = createTimeslotDto.Adapt<TimeSlot>();
        _context.TimeSlots.Add(timeslot);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetTimeslot), new { id = timeslot.TimeSlotId }, timeslot.Adapt<TimeslotDto>());
    }
    
    //PUT    /api/v1/timeslots/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTimeslot(string id, [FromBody] UpdateTimeSlotDto updateTimeslotDto)
    {
        var timeslot = await _context.TimeSlots.FindAsync(id);
        if (timeslot == null)
        {
            return NotFound();
        }
        updateTimeslotDto.Adapt(timeslot);
        await _context.SaveChangesAsync();
        return Ok(timeslot.Adapt<TimeslotDto>());
    }
    
    //DELETE    /api/v1/timeslots/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTimeslot(string id)
    {
        var timeslot = await _context.TimeSlots.FindAsync(id);
        if (timeslot == null)
        {
            return NotFound();
        }
        _context.TimeSlots.Remove(timeslot);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}