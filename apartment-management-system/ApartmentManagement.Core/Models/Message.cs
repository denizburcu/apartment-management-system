using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ApartmentManagement.Core.Models
{
    public class Message : BaseEntity
    {
        public string? Description { get; set; }
        public MessageStatus Status { get; set; }
        public string? UserId { get; set; }
        [ValidateNever]
        public User User { get; set; }
    }

    public enum MessageStatus
    {
        NEW,
        READ,
        NOT_READ
    }
}
