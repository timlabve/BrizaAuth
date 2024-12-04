using Brizaapp.Identity.ViewModels;
using eQualy.P2Pay.Identity.Options;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using static Brizaapp.Identity.Services.AuthenticationService;

namespace Brizaapp.Identity.Factories
{
  public class JwtFactory : IJwtFactory
  {

    private readonly JwtIssuerOptions jwtOptionsValue;

    public JwtFactory(IOptions<JwtIssuerOptions> jwtOptions)
    {
      this.jwtOptionsValue = jwtOptions.Value;
    }

    public async Task<UserInfoDto> GenerateJwt(ClaimsIdentity identity, string userId)
    {

      //TODO: agregar validfor en app json
      var response = new UserInfoDto
      {
        Id = identity.Claims.SingleOrDefault(c => c.Type == CustomClaimNames.UserId)?.Value ?? string.Empty,
        UserName = identity.Name ?? "UnknownUser",
        FullName = identity.FindFirst(ClaimTypes.GivenName)?.Value ?? "NoName",
        Email = identity.FindFirst(ClaimTypes.Email)?.Value ?? "noemail@example.com",
        AuthToken = await GenerateEncodedToken(userId, identity),
        ExpiresIn = (int)jwtOptionsValue.ValidFor.TotalSeconds
      };


      return response;
    }

    /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
    private static long ToUnixEpochDate(DateTime date)
      => (long)Math.Round((date.ToUniversalTime() -
                           new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                          .TotalSeconds);
    public async Task<string> GenerateEncodedToken(string userId, ClaimsIdentity identity)
    {

      //TODO: agregar las busqueda por usuarioid y o roles
      var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.Jti, await jwtOptionsValue.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(jwtOptionsValue.IssuedAt).ToString(), ClaimValueTypes.Integer64),
                identity.FindFirst(ClaimTypes.Name) ?? throw new Exception("Name claim is missing"),
                identity.FindFirst(ClaimTypes.Role) ?? throw new Exception("Role claim is missing")
            };

      var jwt = new JwtSecurityToken(
          issuer: jwtOptionsValue.Issuer,
          claims: claims,
          audience: jwtOptionsValue.Audience,
          notBefore: jwtOptionsValue.NotBefore,
          expires: jwtOptionsValue.Expiration,
        signingCredentials: jwtOptionsValue.SigningCredentials);

      var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

      return encodedJwt;
    }


  }
}
