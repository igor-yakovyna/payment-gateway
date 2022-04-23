using MediatR;

namespace PaymentGateway.Application.Commands
{
    public class ProcessPaymentCommand : IRequest<bool>
    {
        public string CardNumber { get; set; }

        public string ExpiryDate { get; set; }

        public string Cvv { get; set; }

        public double Amount { get; set; }

        public string Currency { get; set; }
    }
}