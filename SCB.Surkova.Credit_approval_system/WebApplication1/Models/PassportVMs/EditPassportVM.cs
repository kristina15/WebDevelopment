using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models.ScanVMs;

namespace WebApplication1.Models.PassportVMs
{
    public class EditPassportVM
    {
        [RegularExpression(@"^\d{4}$")]
        [Display(Name = "Passport series")]
        public string Series { get; set; }

        [RegularExpression(@"^\d{6}$")]
        [Display(Name = "Passport number")]
        public string Number { get; set; }

        public List<EditScanVM> Scans { get; set; }
    }
}