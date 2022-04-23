namespace PaymentGateway.Domain.Exceptions
{
    public class PayoutException : Exception
    {
        public PayoutException()
        {
        }

        public PayoutException(string message)
            : base(message)
        {
        }

        public PayoutException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}