using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSystem.Entities.Model;

public class Service : BaseEntity
{
    [Key] 
    [Required]
    public string ServiceId { get; set; } = Guid.NewGuid().ToString();
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
    public ServiceStatus Status { get; set; }

    // Navigation properties
    [ForeignKey("ProviderId")]
    public Provider Provider { get; set; }
    public ICollection<Booking> Bookings { get; set; }
    
    public enum ServiceStatus
    {
        Active,
        Inactive
    }
}