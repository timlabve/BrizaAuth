using BrizaAuth.Identity.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BrizaAuth.Identity
{
  public static class ServiceExtension
  {
    public static IServiceCollection AddIdentityServices(this IServiceCollection services)
    {
      services.AddScoped<IUserService, UserService>();
      services.AddScoped<IAuthenticationService, AuthenticationService>();
      return services;
    }
  }
}
