using Brizaapp.Database;
using Microsoft.EntityFrameworkCore;

namespace Brizaapp.Api.Extensions
{
  public static class ProgramExtensions
  {
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {

      var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
      services.AddDbContextPool<BrizaContext>(
        dbContextOptions =>
        {
          dbContextOptions.UseNpgsql(configuration.GetConnectionString("BrizaDatabase"))
                    .EnableDetailedErrors();

          //Solo en caso que se necesite en desarrollo
          if (environment == "Development")
          {
            dbContextOptions.EnableSensitiveDataLogging();
          }
        });

      return services;
    }
  }
}
