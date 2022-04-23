using PaymentGateway.Application.Contracts.Models;

namespace PaymentGateway.Application.Contracts
{
    public interface IAcquiringBankService
    {
        Task<BankPaymentResponse> Payout(BankPaymentRequest request);
    }
}