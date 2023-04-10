using Consumer.Domain.Aggregates.OrganizationAggregate;
using Consumer.Domain.Aggregates.UserAggregate;
using Consumer.Infastructure.Repositories;

namespace Consumer.WebApi.Utils.Application
{
    public static class ApplicationDependencyExtensions
    {
        public static IServiceCollection RegisterApplication(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IOrganizationRepository, OrganizationRepository>();
            return services;
        }
    }
}
