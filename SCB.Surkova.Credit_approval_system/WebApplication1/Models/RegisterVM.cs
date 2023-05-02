using System.ComponentModel.DataAnnotations;
using WebApplication1.Models.PassportVMs;

namespace WebApplication1.Models
{
    public class RegisterVM
    {
        [Required]
        [StringLength(200, MinimumLength = 2)]
        //[Remote("IsUserNameAllowed", "Validation")]
        [RegularExpression(@"^(([A-Z]{1}\'?[a-z]{1,23}\-?[a-z]{1,11})|([А-Я]{1}\'?[а-яё]{1,23}\-?[а-яё]{1,11}))$", ErrorMessage = "Invalid name")]
        [Display(Name = "Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 2)]
        //[Remote("IsUserNameAllowed", "Validation")]
        [RegularExpression(@"^(([A-Z]{1}\'?[a-z]{1,23}\-?[a-z]{1,11})|([А-Я]{1}\'?[а-яё]{1,23}\-?[а-яё]{1,11}))$", ErrorMessage = "Invalid surname")]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [StringLength(200, MinimumLength = 2)]
        //[Remote("IsUserNameAllowed", "Validation")]
        [RegularExpression(@"(^(([A-Z]{1}\'?[a-z]{1,23}\-?[a-z]{1,11})|([А-Я]{1}\'?[а-яё]{1,23}\-?[а-яё]{1,11}))$)*", ErrorMessage = "Invalid patronymic")]
        [Display(Name = "Patronymic*")]
        public string Patronymic { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string Login { get; set; }

        [Required]
        [StringLength(200)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Password confirmation")]
        [DataType(DataType.Password)]
        public string PasswordConfirmation { get; set; }

        [Required]
        public CreatePassportVM Passport { get; set; }
    }
}