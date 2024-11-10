using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BookingSystem.Entities.Model;

namespace BookingSystem.API.DataTransferObjects;
public class CreateServiceDto
{
    [Required]
    public string ProviderId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int Duration { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

}

public class UpdateServiceDto
{
    [Required]
    public string ProviderId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int Duration { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    
    [Required]
    public Service.ServiceStatus Status { get; set; }
}
public class ServiceDto
{
    [Key] 
    [Required]
    public string ServiceId { get; set; } 
    [Required]
    public string ProviderId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int Duration { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    [Required]
    public Service.ServiceStatus Status { get; set; }
}