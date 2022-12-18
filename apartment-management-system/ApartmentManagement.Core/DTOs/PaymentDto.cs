using System.ComponentModel.DataAnnotations;


namespace ApartmentManagement.Core.DTOs
{
    public class PaymentDto
    {
        [Required]
        [StringLength(16)]
        public string CardNumber { get; set; } = null!;

        [Required]
        [StringLength(3)]
        public string CvcCvv { get; set; } = null!;

        [Required]
        public string ExpireDate { get; set; } = null!;
        [Required]
        public decimal PaidAmount { get; set; }
    }
}
