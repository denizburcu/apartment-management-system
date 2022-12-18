using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ApartmentManagement.Core.Models
{
    public class Apartment : BaseEntity
    {
        public int ApartmentNumber { get; set; }
        public int BlockNumber { get; set; }
        public int Floor { get; set; }
        public string Type { get; set; } = null!;
        public Status Status { get; set; } = Status.EMPTY;
        public string? UserId { get; set; }
        [ValidateNever]
        public User User { get; set; }
        [ValidateNever]
        public ICollection<ApartmentCost> ApartmentCosts { get; set; }

    }

    public enum Status
    {
        EMPTY,  
        FULL
    }
}
