using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models.ScanVMs;

namespace WebApplication1.Models.PassportVMs
{
    public class DisplayPassportVM
    {
        public int Id { get; set; }

        [Display(Name = "Passport series")]
        public string Series { get; set; }

        [Display(Name = "Passport number")]
        public string Number { get; set; }

        [Display(Name = "Passport scans")]
        [DisplayFormat(NullDisplayText = "(not denied)")]
        public List<DisplayScanVM> Scans { get; set; }
    }
}