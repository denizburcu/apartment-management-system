using System.Runtime.Serialization;

namespace CreditCardService.Services
{
    public class CreditCardException : Exception
    {

        public CreditCardException(string? message) : base(message)
        {
        }
    }
}