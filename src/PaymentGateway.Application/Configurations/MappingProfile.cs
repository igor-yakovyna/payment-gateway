using AutoMapper;
using PaymentGateway.Application.Commands;
using PaymentGateway.Application.Contracts.Models;
using PaymentGateway.Application.Queries;
using PaymentGateway.Domain.Aggregates;

namespace PaymentGateway.Application.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Payment, PaymentDetailsViewModel>()
                .ForMember(d => d.CardNumber, o =>
                    o.MapFrom(s => $"************{s.CardNumber.Trim().Substring(12, 4)}"))
                .ForMember(d => d.CardExpirationDate, o =>
                    o.MapFrom(s => s.ExpiryDate))
                .ForMember(d => d.Amount, o =>
                    o.MapFrom(s => $"{s.Amount:0.00} {s.Currency}"))
                .ForMember(d => d.PaymentResult, o =>
                    o.MapFrom(s => s.Status.ToString()));

            CreateMap<ProcessPaymentCommand, BankPaymentRequest>();
        }
    }
}