using System.ComponentModel.DataAnnotations;

namespace SCB.Surkova.CreditApprovalSystem.Api.Models.PassportVMs
{
    public class CreatePassportVM
    {
        [Required]
        [RegularExpression(@"^\d{4}$")]
        [Display(Name ="Passport series")]
        public string Series { get; set; }

        [Required]
        [RegularExpression(@"^\d{6}$")]
        [Display(Name = "Passport number")]
        public string Number { get; set; }
    }
}