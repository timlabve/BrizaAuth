using System.Security.Claims;

namespace BrizaAuth.Identity.Services
{
  public interface IAuthenticationService
  {
    Task<ClaimsIdentity?> AuthenticateAsync(string userName, string password, string authenticationType);
  }
}
