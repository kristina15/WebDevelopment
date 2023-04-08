using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using SCB.Surkova.CreditApprovalSystem.Entities;
using SCB.Surkova.CreditApprovalSystem.Validation.Inter;
using System;
using System.Text;
using System.Text.RegularExpressions;

namespace SCB.Surkova.CreditApprovalSystem.Validation
{
    public class PassportValidator : AbstractValidator<Passport>, IPassportValidator
    {
        public PassportValidator()
        {
            RuleSet("Default", () =>
            {
                RuleFor(passport => passport).NotNull().WithMessage("Invalid passport");
            });

            RuleSet("Series and number", () =>
            {
                RuleFor(passport => passport.Series).Matches(@"^\d{4}$", RegexOptions.None).WithMessage("Uncorrect series");
                RuleFor(passport => passport.Number).Matches(@"^\d{6}$", RegexOptions.None).WithMessage("Uncorrect number");
            });
        }

        public ValidationResult Validate(Passport item, Action<ValidationStrategy<Passport>> options)
        {
            return this.Validate<Passport>(item, options);
        }
    }
}
