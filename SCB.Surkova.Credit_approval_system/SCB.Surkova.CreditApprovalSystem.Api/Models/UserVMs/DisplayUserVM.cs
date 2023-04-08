﻿using SCB.Surkova.CreditApprovalSystem.Api.Models.PassportVMs;
using SCB.Surkova.CreditApprovalSystem.Api.Models.ScanVMs;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCB.Surkova.CreditApprovalSystem.Api.Models.User
{
    public class DisplayUserVM
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string FirstName { get; set; }

        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Display(Name = "Patronymic")]
        [DisplayFormat(NullDisplayText ="(not denied)")]
        public string Patronymic { get; set; }

        public string Login { get; set; }

        public DisplayPassportVM  Passport { get; set; }

        [Display(Name ="Additional file")]
        public DisplayScanVM AdditionalFile { get; set; }

        public List<string> Roles { get; set; }
    }
}