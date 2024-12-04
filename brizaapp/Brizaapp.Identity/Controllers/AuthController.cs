using Brizaapp.Identity.Services;
using Brizaapp.Identity.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Brizaapp.Identity.Controllers
{
  public class AuthController : ControllerBase
  {

    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
      _userService = userService;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginAsync([FromBody] LoginViewModel model, CancellationToken cancellationToken = default)
    {
      var result = await _userService.LoginAsync(model, cancellationToken);
      return Ok(result);
    }
  }
}
