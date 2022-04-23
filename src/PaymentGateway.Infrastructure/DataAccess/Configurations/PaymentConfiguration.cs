using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentGateway.Domain.Aggregates;

namespace PaymentGateway.Infrastructure.DataAccess.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.CardNumber)
                .IsRequired();

            builder.Property(e => e.ExpiryDate)
                .IsRequired();

            builder.Property(e => e.Amount)
                .IsRequired();

            builder.Property(e => e.Currency)
                .IsRequired();
        }
    }
}