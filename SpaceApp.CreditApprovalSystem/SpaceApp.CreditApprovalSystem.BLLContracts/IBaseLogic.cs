using FluentValidation.Results;

namespace SpaceApp.CreditApprovalSystem.BLLContracts;

public interface IBaseLogic
{
    void GetValidationException(ValidationResult result);
}
