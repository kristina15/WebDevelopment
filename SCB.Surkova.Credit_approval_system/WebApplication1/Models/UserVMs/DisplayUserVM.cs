using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models.PassportVMs;
using WebApplication1.Models.ScanVMs;

namespace WebApplication1.Models.UserVMs
{
    public class DisplayUserVM
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string FirstName { get; set; }

        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Display(Name = "Patronymic")]
        [DisplayFormat(NullDisplayText = "(not denied)")]
        public string Patronymic { get; set; }

        public string Login { get; set; }

        public DisplayPassportVM Passport { get; set; }

        [Display(Name = "Additional file")]
        public DisplayScanVM AdditionalFile { get; set; }

        public List<string> Roles { get; set; }
    }
}