using CAMS_BCA.Application.Common.Interfaces;
using CAMS_BCA.Infrastructure.Auctions.Persistence;
using CAMS_BCA.Infrastructure.Common;
using CAMS_BCA.Infrastructure.Vehicles.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CAMS_BCA.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddHttpContextAccessor()
                .AddServices()
                .AddAuthorization()
                .AddPersistence();

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services;
        }

        private static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source = CAMS-BCA.sqlite"));
            services.AddScoped<IVehiclesRepository, VehiclesRepository>();
            services.AddScoped<IAuctionsRepository, AuctionsRepository>();
            services.AddScoped<IBidsRepository, BidsRepository>();
            return services;
        }
    }
}