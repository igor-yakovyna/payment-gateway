namespace PaymentGateway.Application.Contracts.Models
{
    public class BankPaymentResponse
    {
        public string TransactionId { get; set; }

        public bool Success { get; set; }
    }
}