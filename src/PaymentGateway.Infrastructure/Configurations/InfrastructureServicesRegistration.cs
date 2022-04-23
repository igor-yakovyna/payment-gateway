using Microsoft.Extensions.DependencyInjection;
using PaymentGateway.Application.Contracts;
using PaymentGateway.Domain.Aggregates;
using PaymentGateway.Infrastructure.Repositories;
using PaymentGateway.Infrastructure.Services;

namespace PaymentGateway.Infrastructure.Configurations
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IPaymentRepository, PaymentRepository>();

            services.AddTransient<IAcquiringBankService, AcquiringBankService>();

            return services;
        }
    }
}