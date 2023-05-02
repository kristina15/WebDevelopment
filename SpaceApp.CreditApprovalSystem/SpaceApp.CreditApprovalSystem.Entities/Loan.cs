using SpaceApp.CreditApprovalSystem.Entities.Enum;

namespace SpaceApp.CreditApprovalSystem.Entities;

public class Loan
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public long Sum { get; set; }

    public StatusTypesEnum Status { get; set; }

    public DateTime DateCreate { get; set; }

    public int PassportId { get; set; }

    public int AdditionalScanId { get; set; }
}
