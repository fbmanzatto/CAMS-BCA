using CAMS_BCA.Application.Common.Behaviors;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

namespace CAMS_BCA.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

                options.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            services.AddValidatorsFromAssemblyContaining(typeof(DependencyInjection));
            return services;
        }
    }
}