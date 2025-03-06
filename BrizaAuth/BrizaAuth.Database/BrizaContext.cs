using BrizaAuth.Database.Identity;
using BrizaAuth.Database.Subscriptions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BrizaAuth.Utils;
using Microsoft.AspNetCore.Identity;

namespace BrizaAuth.Database
{
    public class BrizaContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, IdentityUserLogin<int>, RoleClaim, IdentityUserToken<int>>
    {
        public BrizaContext(DbContextOptions<BrizaContext> options) : base(options) { }

        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<SubscriptionUser> SubscriptionUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.RemovePluralisingTableNameConvention();
            BuildIdentitySchema(modelBuilder);
            modelBuilder.SetNamespaceSchema();
        }

        private void BuildIdentitySchema(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(appUser =>
            {
                appUser.Property(y => y.Disabled).HasDefaultValue(false);
                appUser.HasOne(y => y.CreatedBy).WithMany().HasForeignKey(y => y.CreatedById).OnDelete(DeleteBehavior.Restrict);
                appUser.HasOne(y => y.ModifiedBy).WithMany().HasForeignKey(y => y.ModifiedById).OnDelete(DeleteBehavior.Restrict);
                appUser.Property(x => x.CreatedById).HasDefaultValue(1);
                appUser.Property(x => x.ModifiedById).HasDefaultValue(1);
            });

            modelBuilder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });


        }

    }

}
