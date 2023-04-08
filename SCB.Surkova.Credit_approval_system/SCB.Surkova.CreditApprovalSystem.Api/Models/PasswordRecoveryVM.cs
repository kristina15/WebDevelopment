using SCB.Surkova.CreditApprovalSystem.Api.Models.PassportVMs;
using System.ComponentModel.DataAnnotations;

namespace SCB.Surkova.CreditApprovalSystem.Api.Models
{
    public class PasswordRecoveryVM
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string Login { get; set; }

        [Required]
        [StringLength(200)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Compare(nameof(Password))]
        [Display(Name = "Password confirmation")]
        [DataType(DataType.Password)]
        public string PasswordConfirmation { get; set; }

        [Required]
        public CreatePassportVM Passport { get; set; }
    }
}