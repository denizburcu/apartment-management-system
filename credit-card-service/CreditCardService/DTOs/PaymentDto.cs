using System.ComponentModel.DataAnnotations;

namespace CreditCardService.Services
{
    public class PaymentDto
    {
        [Required]
        public string? CardNumber { get; set; }
        [Required]
        public string? CvcCvv { get; set; }
        [Required]
        public string? ExpireDate { get; set; }
        [Required]
        public decimal PaidAmount { get; set; }
    }
}