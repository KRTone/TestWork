using FluentValidation;
using MediatR;
using Producer.Application.Behaviours;
using System.Reflection;

namespace Producer.WebApi.Utils.Application
{
    public static class MediatorDependencyExtensions
    {
        public static IServiceCollection RegisterMediator(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(Producer.Application.AssmeblyHandler).Assembly);

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(Producer.Application.AssmeblyHandler))!);
            });
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
            return services;
        }
    }
}
