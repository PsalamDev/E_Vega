using Microsoft.EntityFrameworkCore;
using Vega.Persistence;
using Vega.Persistence.Interfaces;
using Vega.Persistence.Repository;

namespace Vega.Extension
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddDatabaseService(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<VegaDbContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
