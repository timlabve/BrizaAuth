using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Brizaapp.Database.Identity
{
  public class User : IdentityUser<int>, IAuditItem
  {
    public required string FirstName { get; set; }
    public string? LastName { get; set; }
    [NotMapped]
    public string FullName => $"{FirstName} {LastName}";
    public bool Disabled { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public int? CreatedById { get; set; }
    public virtual User? CreatedBy { get; set; }
    public int? ModifiedById { get; set; }
    public virtual User? ModifiedBy { get; set; }
  }
}
