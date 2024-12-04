using Brizaapp.Identity.ViewModels;
using System.Security.Claims;

namespace Brizaapp.Identity.Services
{
  public interface IUserService
  {
    Task<UserInfoDto> LoginAsync(LoginViewModel model, CancellationToken cancellationToken = default);
  }
}
