using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using SpaceApp.CreditApprovalSystem.Entities;

namespace SpaceApp.CreditApprovalSystem.ModelValidatorContracts;

public interface IScanValidator : IValidator<ScanFile>
{
    ValidationResult Validate(ScanFile item, Action<ValidationStrategy<ScanFile>> options);
}
