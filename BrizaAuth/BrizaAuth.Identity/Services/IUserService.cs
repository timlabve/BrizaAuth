using BrizaAuth.Identity.ViewModels;
using System.Security.Claims;

namespace BrizaAuth.Identity.Services
{
  public interface IUserService
  {
    Task<UserInfoDto> LoginAsync(LoginViewModel model, CancellationToken cancellationToken = default);
  }
}
