using PaymentGateway.Application.Contracts;
using PaymentGateway.Application.Contracts.Models;

namespace PaymentGateway.Infrastructure.Services
{
    public class AcquiringBankService : IAcquiringBankService
    {
        public async Task<BankPaymentResponse> Payout(BankPaymentRequest request)
        {
            var random = new Random();

            return await Task.FromResult(new BankPaymentResponse
            {
                TransactionId = Guid.NewGuid().ToString(),
                Success = random.Next(0, 10) > 8
            });
        }
    }
}