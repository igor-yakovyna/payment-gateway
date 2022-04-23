using MediatR;

namespace PaymentGateway.Application.Queries
{
    public class GetPaymentDetailsQuery : IRequest<PaymentDetailsViewModel>
    {
        public Guid PaymentId { get; set; }

        public GetPaymentDetailsQuery(Guid paymentId)
        {
            PaymentId = paymentId ==  default ? throw new ArgumentNullException(nameof(paymentId)) : paymentId;
        }
    }
}