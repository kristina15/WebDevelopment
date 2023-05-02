using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using SpaceApp.CreditApprovalSystem.BLLContracts;
using SpaceApp.CreditApprovalSystem.Entities;
using SpaceApp.CreditApprovalSystem.ModelValidatorContracts;

namespace SpaceApp.CreditApprovalSystem.ModelValidators;

public class LoanValidator : AbstractValidator<Loan>, ILoanValidator
{
    public LoanValidator(IPassportLogic passportLogic)
    {
        RuleSet("Default", () =>
        {
            RuleFor(loan => loan).NotNull().WithMessage("Invalid loan");
        });

        RuleSet("Correct loan", () =>
        {
            RuleFor(loan => loan.PassportId).NotEqual(0).NotEmpty().NotNull().WithMessage("Invalid passport");
            RuleFor(loan => loan.UserId).NotEqual(0).NotEmpty().NotNull().WithMessage("Invalid user");
            RuleFor(loan => loan.AdditionalScanId).NotEqual(0).NotEmpty().NotNull().WithMessage("Invalid additional scan");
            RuleFor(loan => passportLogic.GetPassportById(loan.PassportId)).Custom((passport, context) =>
            {
                if (passport != null)
                {
                    if (passport.Scans == null)
                    {
                        context.AddFailure("Passport don't have scan");
                    }
                }
            });
            RuleFor(loan => loan.Sum).GreaterThan(0).WithMessage("Uncorrect sum");
        });
    }

    public ValidationResult Validate(Loan item, Action<ValidationStrategy<Loan>> options)
    {
        return this.Validate<Loan>(item, options);
    }
}
