using BookingSystem.API.DataTransferObjects;
using BookingSystem.Entities.DbContext;
using BookingSystem.Entities.Model;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProvidersController : ControllerBase
{
    private readonly ILogger<ProvidersController> _logger;
    private readonly BookingSystemDbContext _context;
    public ProvidersController(ILogger<ProvidersController> logger, BookingSystemDbContext context)
    {
        _logger = logger;
        _context = context;
    }
    //GET
    [HttpGet]
    public async Task<IActionResult> GetProviders()
    {
        var providers = await _context.Providers.Select(p => p.Adapt<ProviderDto>()).ToListAsync();
        return Ok(providers);
    }
    
    //GET BY ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProvider(string id)
    {
        var provider = await _context.Providers.FindAsync(id);
        if (provider == null)
        {
            return NotFound();
        }
        return Ok(provider.Adapt<ProviderDto>());
    }
    //GET SERVICES BY PROVIDER ID
    [HttpGet("{id}/services")]
    public async Task<IActionResult> GetServicesByProvider(string id)
    {
        var services = await _context.Services
            .Where(s => s.ProviderId == id)
            .Select(s => s.Adapt<ServiceDto>())
            .ToListAsync();
        return Ok(services);
    }
    
    //GET TIMESLOTS BY PROVIDER ID
    [HttpGet("{id}/timeslots")]
    public async Task<IActionResult> GetTimeslotsByProvider(string id)
    {
        var timeslots = await _context.TimeSlots
            .Where(s => s.ProviderId == id)
            .Select(s => s.Adapt<TimeslotDto>())
            .ToListAsync();
        return Ok(timeslots);
    }
    
    //POST
    [HttpPost]
    public async Task<IActionResult> CreateProvider([FromBody] CreateProviderDto providerDto)
    {
        var provider = providerDto.Adapt<Provider>();
        var res = _context.Providers.Add(provider);
        await _context.SaveChangesAsync();

        var resDto = res.Entity.Adapt<ProviderDto>();  
        
        return CreatedAtAction(nameof(GetProvider), new { id = resDto.ProviderId }, resDto);
    }
    
    //PUT
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProvider(string id, [FromBody] UpdateProviderDto providerDto)
    {
        var provider = await _context.Providers.FindAsync(id);
        if (provider == null)
        {
            return NotFound();
        }
        _context.Entry(provider).CurrentValues.SetValues(providerDto);
        await _context.SaveChangesAsync();
        return Ok(provider.Adapt<ProviderDto>());
    }
    
    //DELETE
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProvider(string id)
    {
        var provider = await _context.Providers.FindAsync(id);
        if (provider == null)
        {
            return NotFound();
        }
        _context.Providers.Remove(provider);
        await _context.SaveChangesAsync();
        return NoContent();
    }
    
}