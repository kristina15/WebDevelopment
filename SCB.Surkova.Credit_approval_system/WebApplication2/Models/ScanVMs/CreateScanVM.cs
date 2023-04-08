﻿using System.ComponentModel.DataAnnotations;

namespace SCB.Surkova.CreditApprovalSystem.Web.Models.ScanVMs
{
    public class CreateScanVM
    {
        [Required]
        public string Type { get; set; }

        [Required]
        public byte[] Image { get; set; }
    }
}