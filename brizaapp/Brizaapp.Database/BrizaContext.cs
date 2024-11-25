using Brizaapp.Database.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Brizaapp.Database
{
    public class BrizaContext : IdentityDbContext<User, Role, int>
    {
        public BrizaContext(DbContextOptions<BrizaContext> options) : base(options) { }
    }
}
