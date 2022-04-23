using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PaymentGateway.Application.Contracts;
using PaymentGateway.Application.Contracts.Models;
using PaymentGateway.Domain.Aggregates;
using PaymentGateway.Domain.Exceptions;

namespace PaymentGateway.Application.Commands
{
    public class ProcessPaymentCommandHandler : IRequestHandler<ProcessPaymentCommand, bool>
    {
        private readonly ILogger<ProcessPaymentCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IAcquiringBankService _acquiringBankService;

        public ProcessPaymentCommandHandler(ILogger<ProcessPaymentCommandHandler> logger,
            IMapper mapper,
            IPaymentRepository paymentRepository,
            IAcquiringBankService acquiringBankService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
            _acquiringBankService = acquiringBankService ?? throw new ArgumentNullException(nameof(acquiringBankService));
        }

        public async Task<bool> Handle(ProcessPaymentCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var paymentRequest = _mapper.Map<ProcessPaymentCommand, BankPaymentRequest>(command);

                var paymentResponse = await _acquiringBankService.Payout(paymentRequest);

                var payment = new Payment(command.CardNumber, command.ExpiryDate, command.Amount, command.Currency, paymentResponse.TransactionId);
                payment.SetPaymentStatus(paymentResponse.Success);

                await _paymentRepository.AddAsync(payment, cancellationToken);

                return paymentResponse.Success;
            }
            catch (Exception e)
            {
                const string message = "An error occurred while trying to process payment.";

                _logger.LogError(e, message);
                throw new PayoutException(message, e);
            }
        }
    }
}