using Consumer.Infastructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Consumer.WebApi.Utils.Infrastructure
{
    public static class DbContexDependencyExtensions
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ConsumerDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("Default"),
                sqlOpts =>
                {
                    sqlOpts.MigrationsAssembly(Assembly.GetAssembly(typeof(Consumer.Migrations.AssmeblyHandler))!.FullName);
                    sqlOpts.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(10), errorNumbersToAdd: null);
                }));
            return services;
        }
    }
}
