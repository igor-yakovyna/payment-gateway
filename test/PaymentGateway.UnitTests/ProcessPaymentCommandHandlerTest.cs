using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PaymentGateway.Application.Commands;
using PaymentGateway.Application.Configurations;
using PaymentGateway.Application.Contracts;
using PaymentGateway.Application.Contracts.Models;
using PaymentGateway.Domain.Aggregates;
using PaymentGateway.Domain.Exceptions;

namespace PaymentGateway.UnitTests
{
    [TestClass]
    public class ProcessPaymentCommandHandlerTest
    {
        private Mock<ILogger<ProcessPaymentCommandHandler>> _loggerMock;
        private IMapper _mapper;
        private Mock<IPaymentRepository> _paymentRepositoryMock;
        private Mock<IAcquiringBankService> _acquiringBankServiceMock;

        [TestInitialize]
        public void Initialize()
        {
            _loggerMock = new Mock<ILogger<ProcessPaymentCommandHandler>>();
            _mapper = new Mapper(new MapperConfiguration(options => options.AddProfile(new MappingProfile())));
            _paymentRepositoryMock = new Mock<IPaymentRepository>();
            _acquiringBankServiceMock = new Mock<IAcquiringBankService>();
        }

        [TestMethod]
        public async Task HandlePayoutReturnsTrue_When_AcquiringBankServiceReturns_SuccessResponse()
        {
            // arrange
            _acquiringBankServiceMock
                .Setup(repository => repository.Payout(It.IsAny<BankPaymentRequest>()))
                .ReturnsAsync(new BankPaymentResponse {TransactionId = "transaction_id", Success = true});

            var processPaymentCommandHandler = CreateNewProcessPaymentCommandHandler();

            // act
            var result = await processPaymentCommandHandler.Handle(new ProcessPaymentCommand(), new CancellationToken());

            // assert
            Assert.IsTrue(result);
            _paymentRepositoryMock
                .Verify(mock => mock.AddAsync(It.IsAny<Payment>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [TestMethod]
        public async Task HandlePayoutReturnsFalse_When_AcquiringBankServiceReturns_NotSuccessResponse()
        {
            // arrange
            _acquiringBankServiceMock
                .Setup(repository => repository.Payout(It.IsAny<BankPaymentRequest>()))
                .ReturnsAsync(new BankPaymentResponse { TransactionId = "transaction_id", Success = false });

            var processPaymentCommandHandler = CreateNewProcessPaymentCommandHandler();

            // act
            var result = await processPaymentCommandHandler.Handle(new ProcessPaymentCommand(), new CancellationToken());

            // assert
            Assert.IsFalse(result);
            _paymentRepositoryMock
                .Verify(mock => mock.AddAsync(It.IsAny<Payment>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [TestMethod]
        public async Task HandlePayoutThrowsAnException_When_AcquiringBankServiceReturns_NotSuccessResponse()
        {
            // arrange
            _acquiringBankServiceMock
                .Setup(repository => repository.Payout(It.IsAny<BankPaymentRequest>()))
                .ThrowsAsync(new Exception());

            var processPaymentCommandHandler = CreateNewProcessPaymentCommandHandler();

            // act
            Task Action() => processPaymentCommandHandler.Handle(new ProcessPaymentCommand(), new CancellationToken());

            // assert
            await Assert.ThrowsExceptionAsync<PayoutException>(Action);
            _paymentRepositoryMock
                .Verify(mock => mock.AddAsync(It.IsAny<Payment>(), It.IsAny<CancellationToken>()), Times.Never());
        }

        private ProcessPaymentCommandHandler CreateNewProcessPaymentCommandHandler()
        {
            return new ProcessPaymentCommandHandler(_loggerMock.Object,
                _mapper,
                _paymentRepositoryMock.Object,
                _acquiringBankServiceMock.Object);
        }
    }
}