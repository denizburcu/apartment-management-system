using CreditCardService.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CreditCardService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditCardController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public CreditCardController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<IActionResult> MakePayment([FromBody] PaymentDto dto)
        {
            if (ModelState.IsValid)
            {
                await _paymentService.MakePayment(dto);
                return Ok();
            }
            return BadRequest();
        }
    }
}
