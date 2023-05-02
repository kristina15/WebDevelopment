using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using SpaceApp.CreditApprovalSystem.Entities;

namespace SpaceApp.CreditApprovalSystem.ModelValidatorContracts;

public interface IPassportValidator : IValidator<Passport>
{
    ValidationResult Validate(Passport item, Action<ValidationStrategy<Passport>> options);
}
