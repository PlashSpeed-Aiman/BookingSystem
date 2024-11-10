using BookingSystem.Entities.Model;

namespace BookingSystem.API.Extensions;

public static class BaseEntityExtension
{
    public static void SetCreated(this BaseEntity entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
    }

    public static void SetUpdated(this BaseEntity entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
    }
}