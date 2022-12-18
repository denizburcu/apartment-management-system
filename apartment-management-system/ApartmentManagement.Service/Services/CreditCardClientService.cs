using ApartmentManagement.Core.DTOs;
using System.Net.Http.Json;


namespace ApartmentManagement.Service.Services
{
    public class CreditCardClientService
    {
        public async Task<bool> MakePayment(PaymentDto paymentDto)
        {
            using(var client = new HttpClient())
            {
                var response = await client.PostAsJsonAsync("http://localhost:52911/api/CreditCard", paymentDto);
                if(response.IsSuccessStatusCode)
                    return true;
                else 
                    return false;
            }
        }

    }
}
