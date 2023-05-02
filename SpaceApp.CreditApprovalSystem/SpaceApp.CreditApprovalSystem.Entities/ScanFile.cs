using SpaceApp.CreditApprovalSystem.Entities.Enum;

namespace SpaceApp.CreditApprovalSystem.Entities;

public class ScanFile
{
    public int Id { get; set; }

    public TitleTypesEnum Title { get; set; }

    public byte[] Link { get; set; }
}
