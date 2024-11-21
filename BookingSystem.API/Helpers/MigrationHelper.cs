using BookingSystem.Entities.DbContext;
using Microsoft.EntityFrameworkCore;

public static class MigrationManager
{
    public static WebApplication MigrateDatabase(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            using (var context = scope.ServiceProvider.GetRequiredService<BookingSystemDbContext>())
            {
                try
                {
                    // Check if there are any pending migrations
                    if (context.Database.GetPendingMigrations().Any())
                    {
                        context.Database.Migrate();
                        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                        logger.LogInformation("Database migrated successfully");
                    }
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating the database.");
                    throw;
                }
            }
        }
        return app;
    }
}
