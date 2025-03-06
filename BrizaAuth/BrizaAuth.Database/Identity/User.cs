using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrizaAuth.Database.Identity
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
        public string? IdentifierValue { get; set; }
        public string? IdentifierMedValue { get; set; }
        public int UserType { get; set; }
        public bool ValidatedUser { get; set; }
        public DateTime? DateValidatedUser { get; set; }
        public bool ValidatedMed { get; set; }
        public DateTime? DateValidatedMed { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = default!;

        public ICollection<UserClaim> UserClaims { get; set; } = default!;
    }
}
