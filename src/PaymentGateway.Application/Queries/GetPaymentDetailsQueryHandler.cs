using AutoMapper;
using MediatR;
using PaymentGateway.Domain.Aggregates;

namespace PaymentGateway.Application.Queries
{
    public class GetPaymentDetailsQueryHandler : IRequestHandler<GetPaymentDetailsQuery, PaymentDetailsViewModel>
    {
        private readonly IMapper _mapper;
        private readonly IPaymentRepository _paymentRepository;

        public GetPaymentDetailsQueryHandler(IMapper mapper,
            IPaymentRepository paymentRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
        }

        public async Task<PaymentDetailsViewModel> Handle(GetPaymentDetailsQuery request, CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository.GetById(request.PaymentId, cancellationToken);

            return _mapper.Map<Payment, PaymentDetailsViewModel>(payment);
        }
    }
}