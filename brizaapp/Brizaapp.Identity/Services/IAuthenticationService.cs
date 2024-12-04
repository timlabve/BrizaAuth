using System.Security.Claims;

namespace Brizaapp.Identity.Services
{
  public interface IAuthenticationService
  {
    Task<ClaimsIdentity?> AuthenticateAsync(string userName, string password, string authenticationType);
  }
}
