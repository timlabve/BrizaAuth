using Brizaapp.Database;
using Microsoft.EntityFrameworkCore;

namespace Brizaapp.Api.Extensions
{
    public static class ProgramExtensions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<BrizaContext>(
                dbContextOptions => dbContextOptions
                    .UseMySql(
                        configuration.GetConnectionString("BrizaDatabase"),
                        ServerVersion.AutoDetect(configuration.GetConnectionString("BrizaDatabase")))
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors());

            return services;
        }
    }
}
