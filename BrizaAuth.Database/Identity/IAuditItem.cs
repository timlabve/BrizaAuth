namespace BrizaAuth.Database.Identity
{
    public interface IAuditItem
    {
        DateTime Created { get; set; }

        DateTime Modified { get; set; }

        int? CreatedById { get; set; }

        User? CreatedBy { get; set; }

        int? ModifiedById { get; set; }

        User? ModifiedBy { get; set; }
    }
}
