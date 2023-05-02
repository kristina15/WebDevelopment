using System.ComponentModel.DataAnnotations;

namespace SpaceApp.CreditApprovalSystem.Web.Models;

public class LoginVM
{
    [Required]
    public string Login { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}