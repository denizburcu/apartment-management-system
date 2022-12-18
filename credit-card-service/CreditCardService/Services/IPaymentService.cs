namespace CreditCardService.Services
{
    public interface IPaymentService
    {
        Task MakePayment(PaymentDto paymentDto);
    }
}
