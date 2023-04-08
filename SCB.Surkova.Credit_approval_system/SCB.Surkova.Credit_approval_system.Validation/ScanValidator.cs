using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using SCB.Surkova.CreditApprovalSystem.Entities;
using SCB.Surkova.CreditApprovalSystem.Validation.Inter;
using System;

namespace SCB.Surkova.CreditApprovalSystem.Validation
{
    public class ScanValidator : AbstractValidator<ScanFile>, IScanValidator
    {
        public ScanValidator()
        {
            RuleSet("Default", () =>
            {
                RuleFor(scan => scan).NotNull().NotEmpty().WithMessage("Invalid scan");
            });

            RuleSet("Title and link", () =>
            {
                RuleFor(scan => scan.Title).IsInEnum().WithMessage("Invalid title of type");
                RuleFor(scan => scan.Link).NotNull().NotEmpty().WithMessage("Invalid link");
            });
        }

        public ValidationResult Validate(ScanFile item, Action<ValidationStrategy<ScanFile>> options)
        {
            return this.Validate<ScanFile>(item, options);
        }
    }
}
