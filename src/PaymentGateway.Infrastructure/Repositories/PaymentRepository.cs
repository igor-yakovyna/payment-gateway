using Microsoft.EntityFrameworkCore;
using PaymentGateway.Domain.Aggregates;

namespace PaymentGateway.Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly DbContext _dataStorageDbContext;
        private readonly DbSet<Payment> _dbSet;

        public PaymentRepository(DbContext dataStorageDbContext)
        {
            _dataStorageDbContext = dataStorageDbContext ?? throw new ArgumentNullException(nameof(dataStorageDbContext));
            _dbSet = _dataStorageDbContext.Set<Payment>();
        }

        public async Task<Payment> GetById(object id, CancellationToken cancellationToken = default)
        {
            var v = _dbSet.ToListAsync(cancellationToken);
            return await _dbSet.FindAsync(new[] {id}, cancellationToken);
        }

        public async Task<Payment> AddAsync(Payment payment, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(payment, cancellationToken);
            await _dataStorageDbContext.SaveChangesAsync(cancellationToken);

            return payment;
        }
    }
}