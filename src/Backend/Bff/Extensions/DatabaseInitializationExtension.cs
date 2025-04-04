using Challenge.Orm;
using Microsoft.EntityFrameworkCore;
using Polly;
using Npgsql; // Adicione esta importação para lidar com NpgsqlException

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

                var retryPolicy = Policy
                    .Handle<NpgsqlException>(ex => ex.IsTransient) // Apenas problemas de conexão transitórios
                    .WaitAndRetry(
                        retryCount: 5,
                        sleepDurationProvider: attempt => TimeSpan.FromSeconds(attempt * 3),
                        onRetry: (exception, timeSpan, attempt, context) =>
                        {
                            logger.LogWarning("Tentativa {Attempt} falhou devido a problema de conexão: {Message}", attempt, exception.Message);
                        });

                try
                {
                    retryPolicy.Execute(() =>
                    {
                        dbContext.Database.EnsureCreated();
                    });
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Database creation failed após múltiplas tentativas.");
                }

                try
                {
                    retryPolicy.Execute(() =>
                    {
                        dbContext.Database.Migrate();
                    });
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Database migration failed após múltiplas tentativas.");
                }
            }
        }
    }
}