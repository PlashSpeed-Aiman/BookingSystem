using System.ComponentModel.DataAnnotations;
using BookingSystem.Entities.Model;

namespace BookingSystem.API.DataTransferObjects;


public class CreateProviderDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Phone { get; set; }
}

public class UpdateProviderDto
{
    public string? ProviderId { get; set; }

    [MaxLength(100)]
    public string Name { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public Provider.ProviderStatus Status { get; set; }
}

public class ProviderDto
{
    public string? ProviderId { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public Provider.ProviderStatus Status { get; set; }
}