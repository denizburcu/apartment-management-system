using CreditCardService.Collections;
using CreditCardService.Models;
using MongoDB.Driver;

namespace CreditCardService.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ICreditCardCollection _cardCollection;

        public PaymentService(ICreditCardCollection cardCollection)
        {
            _cardCollection = cardCollection;
        }

        public async Task MakePayment(PaymentDto paymentDto)
        {
            var creditCard = await _cardCollection.FindByCondition(credit => credit.CardNumber == paymentDto.CardNumber);

            if (creditCard == null)
                throw new CreditCardException("Card number invalid");

            if (creditCard.ExpireDate != paymentDto.ExpireDate)
                throw new CreditCardException("Expire date invalid");

            if (creditCard.CvvCvs != paymentDto.CvcCvv)
                throw new CreditCardException("Cvv invalid");

            if (creditCard.Balance < paymentDto.PaidAmount)
                throw new CreditCardException("Insufficient Balance");

            creditCard.Balance -= paymentDto.PaidAmount;
            await _cardCollection.UpdateByCondition(credit => credit.Id == creditCard.Id, creditCard);
        }
    }
}
