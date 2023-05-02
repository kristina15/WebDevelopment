using FluentValidation;
using FluentValidation.Results;
using SpaceApp.CreditApprovalSystem.BLLContracts;
using System.Text;

namespace SCB.Surkova.Credit_approval_system.BLL;

public abstract class BaseLogic : IBaseLogic
{
    public void GetValidationException(ValidationResult result)
    {
        if (!result.IsValid)
        {
            var builder = new StringBuilder();
            foreach (var item in result.Errors)
            {
                builder.Append(item.ErrorMessage + '\n');
            }

            throw new ValidationException(builder.ToString());
        }
    }
}
