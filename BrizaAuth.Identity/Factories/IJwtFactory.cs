using BrizaAuth.Identity.ViewModels;
using System.Security.Claims;

namespace BrizaAuth.Identity.Factories
{
  public interface IJwtFactory
  {
    Task<UserInfoDto> GenerateJwt(ClaimsIdentity identity, string userId);
  }
}
