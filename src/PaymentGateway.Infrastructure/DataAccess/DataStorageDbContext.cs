using Microsoft.EntityFrameworkCore;
using PaymentGateway.Domain.Aggregates;
using PaymentGateway.Infrastructure.DataAccess.Configurations;

namespace PaymentGateway.Infrastructure.DataAccess
{
    public class DataStorageDbContext : DbContext
    {
        public DataStorageDbContext(DbContextOptions<DataStorageDbContext> options)
            : base(options)
        {
        }

        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PaymentConfiguration());
        }
    }
}