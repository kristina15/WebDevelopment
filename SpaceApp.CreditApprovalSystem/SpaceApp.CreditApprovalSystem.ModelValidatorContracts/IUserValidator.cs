using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using SpaceApp.CreditApprovalSystem.Entities;

namespace SpaceApp.CreditApprovalSystem.ModelValidatorContracts;

public interface IUserValidator : IValidator<User>
{
    void ValidateNewRole(User item, string newRole);
    ValidationResult Validate(User item, Action<ValidationStrategy<User>> options);
}