using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using SpaceApp.CreditApprovalSystem.Entities;

namespace SpaceApp.CreditApprovalSystem.ModelValidatorContracts;

public interface ILoanValidator : IValidator<Loan>
{
    ValidationResult Validate(Loan item, Action<ValidationStrategy<Loan>> options);
}
