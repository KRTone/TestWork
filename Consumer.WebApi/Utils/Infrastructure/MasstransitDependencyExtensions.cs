using Consumer.WebApi.Consumers;
using MassTransit;
using MassTransit.Configuration;

namespace Consumer.WebApi.Utils.Infrastructure
{
    public static class MasstransitDependencyExtensions
    {
        public static IServiceCollection AddMasstransit(this IServiceCollection services)
        {
            services.AddMassTransit(busCfg =>
            {
                busCfg.RegisterConsumer<UserConsumer>();
                busCfg.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("rabbitmq");
                    cfg.ReceiveEndpoint("user-create", x =>
                    {
                        x.Consumer<UserConsumer>(context);
                    });
                });
            });
            return services;
        }
    }
}
