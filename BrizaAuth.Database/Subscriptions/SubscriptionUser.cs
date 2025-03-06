using BrizaAuth.Database.Identity;

namespace BrizaAuth.Database.Subscriptions
{
  public class SubscriptionUser
  {
    public Guid Id { get; set; } 
    public required int UserId { get; set; }
    public required User User { get; set; }
    public Guid SubscriptionId { get; set; } 
    public required string PlanName { get; set; } 
    public int? CreatedById { get; set; } 
    public DateTime Created { get; set; } 
    public DateTime? Modified { get; set; } 
    public required string Params { get; set; } 
    public required Subscription Subscription { get; set; } 
    public required User CreatedBy { get; set; } 
  }
}
