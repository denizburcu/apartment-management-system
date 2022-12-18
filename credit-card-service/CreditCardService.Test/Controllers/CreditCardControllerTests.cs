using CreditCardService.Controllers;
using CreditCardService.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CreditCardService.Test.Controllers
{
    public class CreditCardControllerTests
    {
        private readonly Mock<IPaymentService> _mockPaymentService;
        private readonly CreditCardController _creditCardController;
        private PaymentDto _paymentDto;
        private PaymentDto _badRequestPaymentDto;
        public CreditCardControllerTests()
        {
            _mockPaymentService = new Mock<IPaymentService>();
            _creditCardController = new CreditCardController(_mockPaymentService.Object);
            _paymentDto = new PaymentDto()
            {
                CardNumber = "1234567891234567",
                CvcCvv = "123",
                ExpireDate = "12/20",
                PaidAmount = 123
            };
        }

        [Fact]
        public async void MakePayment_ActionExecutes_ReturnOk()
        {
            //arrange
            _mockPaymentService.Setup(u => u.MakePayment(_paymentDto)).Returns(Task.CompletedTask);
            //act
            var result = await _creditCardController.MakePayment(_paymentDto);
            //assert
            Assert.IsType<OkResult>(result);
            _mockPaymentService.Verify(p => p.MakePayment(_paymentDto), Times.Once);
        }

        [Fact]
        public async void MakePayment_ActionExecutes_ReturnBadRequest()
        {
            //arrange
            _creditCardController.ModelState.AddModelError("error", "error");
            //act
            var result = await _creditCardController.MakePayment(_badRequestPaymentDto);
            //assert
            Assert.IsType<BadRequestResult>(result);
            _mockPaymentService.Verify(p => p.MakePayment(_paymentDto), Times.Never);
        }
    }
}
