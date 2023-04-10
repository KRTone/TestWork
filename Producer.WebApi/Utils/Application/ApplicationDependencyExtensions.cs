using Producer.Domain.Aggregates.UserAggregate;
using Producer.Domain.Interfaces;
using Producer.Infrastructure.RabbitMqProducers;

namespace Producer.WebApi.Utils.Application
{
    public static class ApplicationDependencyExtensions
    {
        public static IServiceCollection RegisterApplication(this IServiceCollection services)
        {
            services.AddScoped<IPublisher<User>, UserPublisher>();
            return services;
        }
    }
}
