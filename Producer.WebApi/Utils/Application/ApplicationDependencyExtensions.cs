namespace Producer.WebApi.Utils.Application
{
    public static class ApplicationDependencyExtensions
    {
        public static IServiceCollection RegisterApplication(this IServiceCollection services)
        {
            //services.AddScoped<IPublisher<User>, MassTransitPublisher>();
            return services;
        }
    }
}
