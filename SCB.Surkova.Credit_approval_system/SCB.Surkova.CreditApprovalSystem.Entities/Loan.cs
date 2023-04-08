using System;

namespace SCB.Surkova.CreditApprovalSystem.Entities
{
    public class Loan
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public long Sum { get; set; }

        public Status Status { get; set; }

        public DateTime DateCreate { get; set; }

        public int PassportId { get; set; }

        public int AdditionalScanId { get; set; }
    }

    public enum Status : byte
    {
        InWaiting = 1,
        Approved,
        Denied
    }
}
