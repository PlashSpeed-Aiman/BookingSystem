using System.ComponentModel.DataAnnotations;
using BookingSystem.API.DataTransferObjects;

namespace BookingSystem.API.Extensions;

// Alternative using custom validation attribute:
public class EndTimeValidationAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext context)
    {
        var timeSlot = (CreateTimeSlotDto)context.ObjectInstance;
        
        if (timeSlot.StartTime >= timeSlot.EndTime)
        {
            return new ValidationResult(
                "Start time must be earlier than end time",
                new[] { context.MemberName }
            );
        }

        return ValidationResult.Success;
    }
}