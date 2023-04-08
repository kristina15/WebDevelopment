using System.ComponentModel.DataAnnotations;

namespace SCB.Surkova.CreditApprovalSystem.Api.Models.LoanVMs
{
    public class CreateLoanVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Amount of loans must be more than 0")]
        [Range(minimum:0, maximum:long.MaxValue, ErrorMessage ="Uncorrect sum")]
        public long Sum { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required(ErrorMessage ="Invalid passport")]
        public int PassportId { get; set; }

        [Required(ErrorMessage = "Invalid additional scan file")]
        public int? AdditionalScanId { get; set; }

        [Required(ErrorMessage = "Invalid passport scan")]
        public int? PassportScanId { get; set; }
    }
}