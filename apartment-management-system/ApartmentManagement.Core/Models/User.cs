using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ApartmentManagement.Core.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string IdentityNumber { get; set; } = null!;
        public string? PlateNumber { get; set; }
        [ValidateNever]
        public Apartment Apartment { get; set; }
        [ValidateNever]
        public ICollection<Message> Message { get; set; }
    }
}
