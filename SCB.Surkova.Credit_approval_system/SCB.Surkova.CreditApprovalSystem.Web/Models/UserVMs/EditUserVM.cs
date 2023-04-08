﻿using SCB.Surkova.CreditApprovalSystem.Entities;
using SCB.Surkova.CreditApprovalSystem.Web.Models.PassportVMs;
using SCB.Surkova.CreditApprovalSystem.Web.Models.ScanVMs;
using System.ComponentModel.DataAnnotations;

namespace SCB.Surkova.CreditApprovalSystem.Web.Models.UserVMs
{
    public class EditUserVM
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        [RegularExpression(@"^(([A-Z]{1}\'?[a-z]{1,23}\-?[a-z]{1,11})|([А-Я]{1}\'?[а-яё]{1,23}\-?[а-яё]{1,11}))$", ErrorMessage = "Invalid name")]
        public string FirstName { get; set; }

        [Display(Name = "Surname")]
        [RegularExpression(@"^(([A-Z]{1}\'?[a-z]{1,23}\-?[a-z]{1,11})|([А-Я]{1}\'?[а-яё]{1,23}\-?[а-яё]{1,11}))$", ErrorMessage = "Invalid name")]
        public string Surname { get; set; }

        [Display(Name = "Patronymic")]
        [DisplayFormat(NullDisplayText = "(not denied)")]
        [RegularExpression(@"(^(([A-Z]{1}\'?[a-z]{1,23}\-?[a-z]{1,11})|([А-Я]{1}\'?[а-яё]{1,23}\-?[а-яё]{1,11}))$)*", ErrorMessage = "Invalid patronymic")]
        public string Patronymic { get; set; }

        public EditPassportVM Passport { get; set; }

        [Display(Name = "Additional file")]
        public EditScanVM AdditionalFile { get; set; }
    }
}