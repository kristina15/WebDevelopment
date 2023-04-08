using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using SCB.Surkova.CreditApprovalSystem.Entities;
using System;

namespace SCB.Surkova.CreditApprovalSystem.Validation.Inter
{
    public interface ILoanValidator : IValidator<Loan>
    {
        ValidationResult Validate(Loan item, Action<ValidationStrategy<Loan>> options);
    }
}
