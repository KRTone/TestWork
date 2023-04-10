using Consumer.Infastructure.DataBase;
using Microsoft.Data.SqlClient;
using Polly.Retry;
using Polly;
using Microsoft.EntityFrameworkCore;
using Consumer.Domain.Aggregates.OrganizationAggregate;

namespace Consumer.WebApi.Utils.Infrastructure
{
    public class ConsumerContexSeed
    {
        public async Task SeedAsync(ConsumerDbContext context, ILogger<ConsumerContexSeed> logger)
        {
            var policy = CreatePolicy(logger, nameof(ConsumerContexSeed));

            await policy.ExecuteAsync(async () =>
            {
                using (context)
                {
                    context.Database.Migrate();

                    if (!context.Organizations.Any())
                    {
                        await context.Organizations.AddRangeAsync(
                            new Organization(Guid.NewGuid(), "Organization for Development 1"),
                            new Organization(Guid.NewGuid(), "Organization for Development 1"),
                            new Organization(Guid.NewGuid(), "Organization for Development 1"));
                        await context.SaveChangesAsync();
                    }

                    //if(!context.Organization.Any())
                    //{
                    //    await context.Users.AddRangeAsync(
                    //        new Domain.Models.User { Name = "Organization for Development 1" },
                    //        new Domain.Models.User { Name = "Organization for Development 2" },
                    //        new Domain.Models.User { Name = "Organization for Development 3" });
                    //    await context.SaveChangesAsync();
                    //}

                    await context.SaveChangesAsync();
                }
            });
        }

        private AsyncRetryPolicy CreatePolicy(ILogger<ConsumerContexSeed> logger, string prefix, int retries = 3)
        {
            return Policy.Handle<SqlException>()
                .WaitAndRetryAsync(
                    retryCount: retries,
                    sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                    onRetry: (exception, timeSpan, retry, ctx) =>
                    {
                        logger.LogWarning(exception, $"[{prefix}] Exception {exception.GetType().Name} with message {exception.Message} detected on attempt {retry} of {retries}");
                    }
                );
        }
    }
}