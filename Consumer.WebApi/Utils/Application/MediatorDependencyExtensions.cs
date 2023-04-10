using Consumer.Application.Behaviours;
using FluentValidation;
using MediatR;
using System.Reflection;

namespace Consumer.WebApi.Utils.Application
{
    public static class MediatorDependencyExtensions
    {
        public static IServiceCollection RegisterMediator(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(Consumer.Application.AssmeblyHandler).Assembly);

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(Consumer.Application.AssmeblyHandler))!);
            });
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));
            return services;
        }
    }
}
