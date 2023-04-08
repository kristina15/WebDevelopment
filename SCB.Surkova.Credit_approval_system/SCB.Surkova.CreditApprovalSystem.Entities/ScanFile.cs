namespace SCB.Surkova.CreditApprovalSystem.Entities
{
    public class ScanFile
    {
        public int Id { get; set; }

        public TypeTitles Title { get; set; }

        public byte[] Link { get; set; }
    }

    public enum TypeTitles
    {
        Passport = 1,
        SNILS,
        DriversLicense
    }
}
