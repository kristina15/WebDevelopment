using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using SCB.Surkova.CreditApprovalSystem.Entities;
using System;

namespace SCB.Surkova.CreditApprovalSystem.Validation.Inter
{
    public interface IPassportValidator : IValidator<Passport>
    {
        ValidationResult Validate(Passport item, Action<ValidationStrategy<Passport>> options);
    }
}
