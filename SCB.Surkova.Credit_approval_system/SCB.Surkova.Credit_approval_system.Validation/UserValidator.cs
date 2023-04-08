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
    public class UserValidator : AbstractValidator<User>, IUserValidator
    {
        public UserValidator()
        {
            RuleSet("Default", () =>
            {
                RuleFor(user => user).NotNull().WithMessage("Invalid user");
                RuleFor(customer => customer.Passport).SetValidator(new PassportValidator());
            });

            RuleSet("FIO", () =>
            {
                RuleFor(customer => customer.FirstName).Matches(@"^(([A-Z]{1}\'?[a-z]{1,23}\-?[a-z]{1,11})|([А-Я]{1}\'?[а-яё]{1,23}\-?[а-яё]{1,11}))$", RegexOptions.None).WithMessage("Invalid name");
                RuleFor(customer => customer.Surname).Matches(@"^(([A-Z]{1}\'?[a-z]{1,23}\-?[a-z]{1,11})|([А-Я]{1}\'?[а-яё]{1,23}\-?[а-яё]{1,11}))$", RegexOptions.None).WithMessage("Invalid surname");
                RuleFor(customer => customer.Patronymic).Matches(@"(^(([A-Z]{1}\'?[a-z]{1,23}\-?[a-z]{1,11})|([А-Я]{1}\'?[а-яё]{1,23}\-?[а-яё]{1,11}))$)*", RegexOptions.None).WithMessage("Invalid patronymic");
                RuleFor(x => x)
                .Custom((x, context) =>
                {
                    string regex = @"^([A-Z]{1}\'?[a-z]{1,23}\-?[a-z]{1,11})$";
                    if (Regex.IsMatch(x.FirstName, regex))
                    {
                        if (!Regex.IsMatch(x.Surname, regex))
                        {
                            context.AddFailure("The first and surname are written in different languages");
                        }
                        else if (x.Patronymic != null)
                        {
                            if (!Regex.IsMatch(x.Patronymic, regex))
                            {
                                context.AddFailure("The first, surname and patronymic are written in different languages");
                            }
                        }
                    }
                    else
                    {
                        if (Regex.IsMatch(x.Surname, regex))
                        {
                            context.AddFailure("The first and surname are written in different languages");
                        }
                        else if (x.Patronymic != null)
                        {
                            if (Regex.IsMatch(x.Patronymic, regex))
                            {
                                context.AddFailure("The first, surname and patronymic are written in different languages");
                            }
                        }
                    }
                });
            });

            RuleSet("Login", () =>
            {
                RuleFor(customer => customer.Login).NotNull().NotEmpty().WithMessage("Invalid login");
            });

            RuleSet("Password", () =>
            {
                RuleFor(customer => customer.HashPassword).NotNull().NotEmpty().WithMessage("Invalid password");
            });

            RuleSet("Scan", () =>
            {
                RuleFor(user => user.AdditionalFile).NotNull().WithMessage("Invalid additional scan");
            });

        }

        public ValidationResult Validate(User item, Action<ValidationStrategy<User>> options)
        {
            return this.Validate<User>(item, options);
        }

        public void ValidateNewRole(User item, string newRole)
        {
            if (item.Roles != null)
            {
                if (item.Roles.Contains(newRole))
                {
                    throw new ValidationException("The user already has such a role");
                }
            }
        }
    }
}