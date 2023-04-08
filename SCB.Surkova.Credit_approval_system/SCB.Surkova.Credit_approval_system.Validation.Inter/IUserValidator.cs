using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using SCB.Surkova.CreditApprovalSystem.Entities;
using System;

namespace SCB.Surkova.CreditApprovalSystem.Validation.Inter
{
    public interface IUserValidator : IValidator<User>
    {
        void ValidateNewRole(User item, string newRole);
        ValidationResult Validate(User item, Action<ValidationStrategy<User>> options);
    }
}