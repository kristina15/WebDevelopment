namespace SpaceApp.CreditApprovalSystem.Entities;

public class Passport
{
    public int Id { get; set; }

    public string Series { get; set; }

    public string Number { get; set; }

    public List<ScanFile> Scans { get; set; }
}
