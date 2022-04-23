using FluentValidation;

namespace PaymentGateway.Application.Commands
{
    public class ProcessPaymentCommandValidator : AbstractValidator<ProcessPaymentCommand>
    {
        public ProcessPaymentCommandValidator()
        {
            RuleFor(e => e.CardNumber)
                .CreditCard()
                    .WithMessage("{CardNumber} is not valid")
                .NotEmpty()
                    .WithMessage("{CardNumber} is required")
                .NotNull();

            RuleFor(e => e.ExpiryDate)
                .NotEmpty()
                    .WithMessage("{ExpiryDate} is required")
                .NotNull();

            RuleFor(e => e.Cvv)
                .NotEmpty()
                    .WithMessage("{Cvv} is required")
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(4);

            RuleFor(e => e.Amount)
                .GreaterThan(0)
                .WithMessage("{Amount} is required");

            RuleFor(e => e.Currency)
                .NotEmpty()
                    .WithMessage("{Currency} is required")
                .NotNull();
        }
    }
}