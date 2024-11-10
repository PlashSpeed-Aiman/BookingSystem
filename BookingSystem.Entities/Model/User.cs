using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BookingSystem.Entities.Model;

public  class ApplicationUser : IdentityUser<Guid>
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    public string Phone { get; set; }

    // Navigation property
    public ICollection<Booking> Bookings { get; set; }
}