using System.ComponentModel.DataAnnotations;
using WebApplication1.Models.PassportVMs;

namespace WebApplication1.Models
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