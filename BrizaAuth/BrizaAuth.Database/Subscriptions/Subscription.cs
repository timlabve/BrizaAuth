using BrizaAuth.Database.Identity;

namespace BrizaAuth.Database.Subscriptions
{
  public class Subscription : IAuditItem
  {
    public Guid Id { get; set; } 
    public required string PlanName { get; set; } 
    public DateTime Created { get; set; } 
    public required string Params { get; set; } 
    public bool Active { get; set; } 
    public int Order { get; set; }
    public DateTime Modified { get; set; }
    public int? CreatedById { get; set; }
    public User? CreatedBy { get; set; }
    public int? ModifiedById { get; set; }
    public User? ModifiedBy { get; set; }
  }
}
