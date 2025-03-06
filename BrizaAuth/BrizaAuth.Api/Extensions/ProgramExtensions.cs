using BrizaAuth.Database;
using BrizaAuth.Database.Identity;
using BrizaAuth.Identity;
using BrizaAuth.Identity.Factories;
using BrizaAuth.Utils.Options;
using eQualy.P2Pay.Identity.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BrizaAuth.Api.Extensions
{

  public static class ProgramExtensions
  {
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
      var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

      services.AddDbContextPool<BrizaContext>(dbContextOptions =>
      {
        dbContextOptions.UseNpgsql(configuration.GetConnectionString("BrizaDatabase"))
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging(false)
            .LogTo(Console.WriteLine, LogLevel.Warning);
      });

      #region options
      //TODO: corregir puerto de entrada 
      var jwtAppSettingOptions = configuration.GetSection(nameof(JwtIssuerOptions)).Get<JwtIssuerOptions>() ?? new JwtIssuerOptions();
      var passwordPolicyOptions = configuration.GetSection(nameof(PasswordPolicyOptions)).Get<PasswordPolicyOptions>() ?? new PasswordPolicyOptions();


      #endregion

      #region Identity Services

      services.AddSingleton<IJwtFactory, JwtFactory>();

      //TODO: agregar options idioma
      services
        .AddIdentity<User, Role>(options =>
        {
          options.User.RequireUniqueEmail = true;
          options.Password.RequireDigit = passwordPolicyOptions.RequireDigit;
          options.Password.RequiredLength = passwordPolicyOptions.RequiredLength;
          options.Password.RequireNonAlphanumeric = passwordPolicyOptions.RequireNonAlphanumeric;
          options.Password.RequireUppercase = passwordPolicyOptions.RequireUppercase;
          options.Password.RequireLowercase = passwordPolicyOptions.RequireLowercase;
          options.Password.RequiredUniqueChars = passwordPolicyOptions.RequiredUniqueChars;
        })
        .AddRoleManager<RoleManager<Role>>()
        .AddEntityFrameworkStores<BrizaContext>()
        .AddDefaultTokenProviders();



      services.AddIdentityServices();  
      #endregion

      return services;
    }
  }
}
