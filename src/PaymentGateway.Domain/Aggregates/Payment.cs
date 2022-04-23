using PaymentGateway.Domain.Common;

namespace PaymentGateway.Domain.Aggregates
{
    public class Payment : Entity
    {
        public string CardNumber { get; private set; }

        public string ExpiryDate { get; private set; }

        public double Amount { get; private set; }

        public string Currency { get; private set; }

        public string TransactionId { get; private set; }

        public PaymentStatus Status { get; private set; }

        public Payment(string cardNumber, string expiryDate, double amount, string currency, string transactionId)
        {
            CardNumber = cardNumber;
            ExpiryDate = expiryDate;
            Amount = amount;
            Currency = currency;
            TransactionId = transactionId;
        }

        public void SetPaymentStatus(bool success)
        {
            Status = success ? PaymentStatus.Success : PaymentStatus.Failure;
        }
    }
}