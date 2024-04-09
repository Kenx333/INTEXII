using System.ComponentModel.DataAnnotations;
namespace INTEXII.Models
{
    public class TwoFactor
    {
        [Required]
        public string TwoFactorCode { get; set; }
    }
}
