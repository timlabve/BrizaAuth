using Brizaapp.Identity.ViewModels;
using System.Security.Claims;

namespace Brizaapp.Identity.Factories
{
  public interface IJwtFactory
  {
    Task<UserInfoDto> GenerateJwt(ClaimsIdentity identity, string userId);
  }
}
