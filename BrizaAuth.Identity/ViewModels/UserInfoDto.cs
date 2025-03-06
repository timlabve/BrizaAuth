using System.Globalization;

namespace BrizaAuth.Identity.ViewModels
{
  public class UserInfoDto
  {
    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string AuthToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public double ExpiresIn { get; set; }
  }
}
