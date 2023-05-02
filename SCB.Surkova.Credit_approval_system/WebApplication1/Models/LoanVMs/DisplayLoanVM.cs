using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.LoanVMs
{
    public class DisplayLoanVM
    {
        public int Id { get; set; }

        [Display(Name = "Amount")]
        public long Sum { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Date create")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateCreate { get; set; }
    }
}