using BrizaAuth.Database;
using BrizaAuth.Database.Identity;
using BrizaAuth.Identity.Factories;
using BrizaAuth.Identity.ViewModels;
using BrizaAuth.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BrizaAuth.Identity.Services
{
  public class UserService : IUserService
  {
    private readonly BrizaContext _context;
    private readonly IAuthenticationService _authenticationService;
    private readonly UserManager<User> _userManager;
    private readonly IJwtFactory _jwtFactory;
    private readonly IHttpContextAccessor httpContextAccessor;

    public UserService(BrizaContext context, 
      IAuthenticationService authenticationService, 
      UserManager<User> userManager, 
      IJwtFactory jwtFactory, 
      IHttpContextAccessor httpContextAccessor)
    {
      _context = context;
      _authenticationService = authenticationService;
      _userManager = userManager;
      _jwtFactory = jwtFactory;
      this.httpContextAccessor = httpContextAccessor;
    }

    public async Task<UserInfoDto> LoginAsync(LoginViewModel model, CancellationToken cancellationToken = default)
    {
      var identity = await _authenticationService.AuthenticateAsync(model.UserName, model.Password, JwtBearerDefaults.AuthenticationScheme);

      if (identity == null)
      {
        throw new UserDataException("Usuario o contraseña incorrecta.");
      }

      var nameIdentifier = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new InvalidOperationException("NameIdentifier claim is missing.");


      var jwt = await _jwtFactory.GenerateJwt(identity, nameIdentifier);

      httpContextAccessor.HttpContext?.Response.Cookies.Append("authToken", jwt.AuthToken, new CookieOptions
      {
        HttpOnly = true,
        Expires = DateTime.Now.AddSeconds(jwt.ExpiresIn)
      });

      return jwt;
    }
  }
}
