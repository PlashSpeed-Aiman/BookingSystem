using BookingSystem.API.DataTransferObjects;
using BookingSystem.Entities.DbContext;
using BookingSystem.Entities.Model;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ServicesController : ControllerBase
{
    private readonly ILogger<ServicesController> _logger;
    private readonly BookingSystemDbContext _context;
    public ServicesController(ILogger<ServicesController> logger, BookingSystemDbContext context)
    {
        _logger = logger;
        _context = context;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetServices()
    {
        var services = await _context.Services.Select(s => s.Adapt<ServiceDto>()).ToListAsync();
        return Ok(services);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetServicesById(string id)
    {
        var service = await _context.Services.FindAsync(id);
        if (service == null)
        {
            return NotFound();
        }
        return Ok(service.Adapt<ServiceDto>());
    }

    [HttpPost]
    public async Task<IActionResult> CreateService(CreateServiceDto createServiceDto)
    {
        var service = createServiceDto.Adapt<Service>();
        _context.Services.Add(service);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetServicesById), new { id = service.ServiceId }, service.Adapt<ServiceDto>());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteService(string id)
    {
    var service = await _context.Services.FindAsync(id);
    if (service == null)
    {
        return NotFound();
    }
    _context.Services.Remove(service);
    await _context.SaveChangesAsync();
    return NoContent();
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateService(string id, UpdateServiceDto updateServiceDto)
    {
        var service = await _context.Services.FindAsync(id);
        if (service == null)
        {
            return NotFound();
        }
        updateServiceDto.Adapt(service);
        await _context.SaveChangesAsync();
        return Ok(service.Adapt<ServiceDto>());
    }
}