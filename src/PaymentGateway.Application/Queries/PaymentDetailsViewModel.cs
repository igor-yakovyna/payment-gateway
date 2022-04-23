namespace PaymentGateway.Application.Queries
{
    public class PaymentDetailsViewModel
    {
        public string TransactionId { get; set; }

        public string CardNumber { get; set; }

        public string CardExpirationDate { get; set; }

        public string Amount { get; set; }

        public string PaymentResult { get; set; }
    }
}