using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CreditCardService.Test.Controllers
{
    public class CreditCardControllerIntegrationTests : IClassFixture<Program>
    {
        private readonly HttpClient _client;

        public CreditCardControllerIntegrationTests()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _client = webAppFactory.CreateClient();
        }

        [Fact]
        public async Task MakePayment_ReturnsOk()
        {
            var paymentRequest = new HttpRequestMessage(HttpMethod.Post, "/api/CreditCard");

            var payment = "{\r\n  \"cardNumber\": \"378282246310005\"," +
                            "\r\n  \"cvcCvv\": \"123\"," +
                            "\r\n  \"expireDate\": \"12/30\"," +
                            "\r\n  \"paidAmount\": 123" +
                            "\r\n}";

            paymentRequest.Content = new StringContent(payment, Encoding.UTF8, "application/json");

            var response = await _client.SendAsync(paymentRequest);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task MakePayment_Returns_InvalidExpiryDate()
        {
            var paymentRequest = new HttpRequestMessage(HttpMethod.Post, "/api/CreditCard");

            var payment = "{\r\n  \"cardNumber\": \"378282246310005\"," +
                            "\r\n  \"cvcCvv\": \"123\"," +
                            "\r\n  \"expireDate\": \"12\"," +
                            "\r\n  \"paidAmount\": 123" +
                            "\r\n}";

            paymentRequest.Content = new StringContent(payment, Encoding.UTF8, "application/json");

            var response = await _client.SendAsync(paymentRequest);
            var responseMessage = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("{\"message\":\"Expire date invalid\"}", responseMessage);
        }
    }
}
