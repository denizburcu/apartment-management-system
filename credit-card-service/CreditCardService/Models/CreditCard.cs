using System.ComponentModel.DataAnnotations;

namespace CreditCardService.Models
{
    public class CreditCard : BaseDocument
    {
        [Required]
        public string? CardNumber { get; set; }
        [Required]
        public decimal Balance { get; set; }
        [Required]
        public string? CvvCvs { get; set; }
        [Required]
        public string? ExpireDate { get; set; }
        [Required]
        public string? UserIdentityNumber { get; set; }

    }
}
