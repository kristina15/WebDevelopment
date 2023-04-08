using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using SCB.Surkova.CreditApprovalSystem.Entities;
using System;

namespace SCB.Surkova.CreditApprovalSystem.Validation.Inter
{
    public interface IScanValidator:IValidator<ScanFile>
    {
        ValidationResult Validate(ScanFile item, Action<ValidationStrategy<ScanFile>> options);
    }
}
