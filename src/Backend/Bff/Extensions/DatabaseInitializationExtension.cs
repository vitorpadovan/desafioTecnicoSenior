using Challenge.Orm;
using Microsoft.EntityFrameworkCore;

namespace Bff.Extensions
{
    public static class DatabaseInitializationExtension
    {
        public static void InitDatabase(this IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<AppDbContext>>();
                try
                {
                    dbContext.Database.EnsureCreated();
                }
                catch(Exception ex)
                {
                    logger.LogError(ex,"Database creation failed.");
                }
                try
                {
                    dbContext.Database.Migrate();
                }
                catch(Exception ex)
                {
                    logger.LogError(ex,"Database migration failed.");
                }
            }
        }
    }
}
