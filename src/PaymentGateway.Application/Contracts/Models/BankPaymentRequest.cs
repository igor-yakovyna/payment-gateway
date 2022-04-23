namespace PaymentGateway.Application.Contracts.Models
{
    public class BankPaymentRequest
    {
        public string CardNumber { get; set; }

        public string ExpiryDate { get; set; }

        public string Cvv { get; set; }

        public string Amount { get; set; }

        public string Currency { get; set; } 
    }
}