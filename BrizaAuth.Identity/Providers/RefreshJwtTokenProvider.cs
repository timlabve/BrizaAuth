using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BrizaAuth.Identity.Providers
{
  public class RefreshJwtTokenProvider<TUser> : DataProtectorTokenProvider<TUser> where TUser : class
  {
    public RefreshJwtTokenProvider(IDataProtectionProvider dataProtectionProvider,
           IOptions<RefreshJwtTokenProviderOptions> options,
           ILogger<RefreshJwtTokenProvider<TUser>> logger)
           : base(dataProtectionProvider, options, logger)
    {
    }

    public const string ProviderName = nameof(RefreshJwtTokenProvider<TUser>);
  }

  public class RefreshJwtTokenProviderOptions : DataProtectionTokenProviderOptions
  {
  }
}
