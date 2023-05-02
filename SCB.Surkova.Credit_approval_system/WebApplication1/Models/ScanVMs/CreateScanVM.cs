using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.ScanVMs
{
    public class CreateScanVM
    {
        [Required]
        public string Type { get; set; }

        [Required]
        public byte[] Image { get; set; }
    }
}