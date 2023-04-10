using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Retry;

namespace Consumer.WebApi.Utils.Infrastructure
{
    public static class WebApplicationExtensions
    {
        public static WebApplication MigrateDbContext<TContext>(this WebApplication app, Action<TContext, IServiceProvider> seeder)
            where TContext : DbContext
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<TContext>>();
            var context = services.GetRequiredService<TContext>();

            try
            {
                logger.LogInformation("Migrating database associated with context {DbContextName}", typeof(TContext).Name);

                var retry = CreatePolicy(logger, nameof(TContext));

                //if the sql server container is not created on run docker compose this
                //migration can't fail for network related exception. The retry options for DbContext only 
                //apply to transient exceptions
                // Note that this is NOT applied when running some orchestrators (let the orchestrator to recreate the failing service)
                retry.Execute(() =>
                {
                    context.Database.Migrate();
                    seeder(context, services);
                });


                logger.LogInformation($"Migrated database associated with context {nameof(TContext)}");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"An error occurred while migrating the database used on context {nameof(TContext)}");
            }

            return app;
        }

        private static RetryPolicy CreatePolicy(ILogger logger, string prefix, int retries = 10)
        {
            return Policy.Handle<SqlException>()
                .WaitAndRetry(
                    retryCount: retries,
                    sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    onRetry: (exception, timeSpan, retry, ctx) =>
                    {
                        logger.LogWarning(exception, $"[{prefix}] Exception {exception.GetType().Name} with message {exception.Message} detected on attempt {retry} of {retries}");
                    });
        }
    }
}