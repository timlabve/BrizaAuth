using Microsoft.AspNetCore.Identity;

namespace BrizaAuth.Database.Identity
{
    public class Role : IdentityRole<int>
    {
        public Role() : base() { }

        public Role(string roleName) : base(roleName) { }

        public List<UserRole> UserRoles { get; set; } = default!;

        public List<RoleClaim> RoleClaims { get; set; } = default!;

        public const string Admin = "Admin";
    }
}
