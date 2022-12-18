using CreditCardService.Collections;
using CreditCardService.Models;
using CreditCardService.Services;
using Moq;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace CreditCardService.Test.Services
{
    public class PaymentServiceTests
    {
        private readonly Mock<ICreditCardCollection> _cardCollection;
        private readonly PaymentService _paymentService;
        public PaymentServiceTests()
        {
            _cardCollection = new Mock<ICreditCardCollection>();
            _paymentService = new PaymentService(_cardCollection.Object);
        }

        [Fact]
        public async Task MakePayment_DecreaseBalance_Successfull()
        {
            //arange
            var paymentDto = new PaymentDto()
            {
                CardNumber = "1234567891234567",
                CvcCvv = "123",
                ExpireDate = "12/20",
                PaidAmount = 100
            };
            var creditCard = new CreditCard()
            {
                CardNumber = "1234567891234567",
                CvvCvs = "123",
                ExpireDate = "12/20",
                Balance = 1000

            };
            _cardCollection.Setup(c => c.FindByCondition(It.IsAny<Expression<Func<CreditCard, bool>>>())).ReturnsAsync(creditCard);
            _cardCollection.Setup(c => c.UpdateByCondition(It.IsAny<Expression<Func<CreditCard, bool>>>(), It.IsAny<CreditCard>())).Returns(Task.CompletedTask);
            //act
            await _paymentService.MakePayment(paymentDto);
            //assert
            Assert.Equal(900, creditCard.Balance);
        }

        [Fact]
        public async Task MakePayment_DecreaseBalance_ThrowsInvalidCvvException()
        {
            //arange
            var paymentDto = new PaymentDto()
            {
                CardNumber = "1234567891234567",
                CvcCvv = "123",
                ExpireDate = "12/20",
                PaidAmount = 100
            };
            var creditCard = new CreditCard()
            {
                CardNumber = "1234567891234567",
                CvvCvs = "120",
                ExpireDate = "12/20",
                Balance = 1000

            };
            _cardCollection.Setup(c => c.FindByCondition(credit => credit.CardNumber == paymentDto.CardNumber)).ReturnsAsync(creditCard);
            _cardCollection.Setup(c => c.UpdateByCondition(It.IsAny<Expression<Func<CreditCard, bool>>>(), It.IsAny<CreditCard>())).Returns(Task.CompletedTask);
            //act
            Func<Task> actualException = () => _paymentService.MakePayment(paymentDto);
            //assert
            var exception = await Assert.ThrowsAsync<CreditCardException>(actualException);
            Assert.Equal("Cvv invalid", exception.Message);
        }

    }
}
