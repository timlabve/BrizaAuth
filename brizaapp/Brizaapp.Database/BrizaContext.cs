using Brizaapp.Database.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Brizaapp.Utils;
using Brizaapp.Database.Subscriptions;

namespace Brizaapp.Database
{
  public class BrizaContext : IdentityDbContext<User, Role, int>
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

      
    }

  }





}
