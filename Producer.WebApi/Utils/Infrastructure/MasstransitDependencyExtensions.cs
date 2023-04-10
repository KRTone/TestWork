using MassTransit;

namespace Producer.WebApi.Utils.Infrastructure
{
    public static class MasstransitDependencyExtensions
    {
        public static IServiceCollection AddMasstransit(this IServiceCollection services)
        {
            services.AddMassTransit(busCfg =>
            {
                busCfg.AddPublishObserver<LoggingObserver>();
                busCfg.UsingRabbitMq((_, cfg) =>
                {
                    cfg.Host("rabbitmq");
                });
            });
            return services;
        }
    }
}
