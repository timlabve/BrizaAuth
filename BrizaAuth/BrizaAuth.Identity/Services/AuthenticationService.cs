using BrizaAuth.Database.Identity;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BrizaAuth.Identity.Services
{
  public class AuthenticationService : IAuthenticationService
  {
    private readonly UserManager<User> _userManager;

    public AuthenticationService(UserManager<User> userManager)
    {
      _userManager = userManager;
    }

    public async Task<ClaimsIdentity?> AuthenticateAsync(string userName, string password, string authenticationType)
    {
      if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
      {
        return null;
      }

      var userToVerify = await _userManager.FindByNameAsync(userName);

      if (userToVerify == null || userToVerify.Disabled) return null;

      bool identityCheck = false;
      identityCheck = await _userManager.CheckPasswordAsync(userToVerify, password);
      

      //TODO: generar los include que se pueda necesitar
      if (identityCheck)
      {

      }

      return null;
    }

    public struct CustomClaimNames
    {
      public const string UserId = "user_id";
    }
  }
}
