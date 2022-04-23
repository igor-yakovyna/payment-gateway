namespace PaymentGateway.Domain.Aggregates
{
    public interface IPaymentRepository
    {
        Task<Payment> GetById(object id, CancellationToken cancellationToken = default);

        Task<Payment> AddAsync(Payment payment, CancellationToken cancellationToken = default);
    }
}