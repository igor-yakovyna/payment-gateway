using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PaymentGateway.Application.Configurations;
using PaymentGateway.Application.Queries;
using PaymentGateway.Domain.Aggregates;

namespace PaymentGateway.UnitTests
{
    [TestClass]
    public class GetPaymentDetailsQueryHandlerTest
    {
        private const string CardNumber = "1234123412341234";
        private const string ExpiryDate = "02/24";
        private const double Amount = 80.5;
        private const string Currency = "USD";
        private const string TransactionId = "transaction_id";

        private IMapper _mapper;
        private Mock<IPaymentRepository> _paymentRepositoryMock;

        [TestInitialize]
        public void Initialize()
        {
            _mapper = new Mapper(new MapperConfiguration(options => options.AddProfile(new MappingProfile())));
            _paymentRepositoryMock = new Mock<IPaymentRepository>();
        }

        [TestMethod]
        public async Task HandleGetPaymentDetails_ReturnsPaymentDetailsViewModel_WhenDataStorageContainsAndReturnsPayoutObject()
        {
            // arrange
            var payment = new Payment(CardNumber, ExpiryDate, Amount, Currency, TransactionId);
            payment.SetPaymentStatus(true);
            _paymentRepositoryMock
                .Setup(repository => repository.GetById(It.IsAny<object>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(payment);

            var processPaymentCommandHandler = CreateNewGetPaymentDetailsQueryHandler();

            // act
            var result = await processPaymentCommandHandler.Handle(new GetPaymentDetailsQuery(Guid.Parse("44ba0c32-75d9-4176-98bb-a3de7105da74")), new CancellationToken());

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.TransactionId, TransactionId);
            Assert.AreNotEqual(result.CardNumber, CardNumber);
            Assert.AreEqual(result.CardExpirationDate, ExpiryDate);
            Assert.AreEqual(result.Amount, $"{Amount:0.00} {Currency}");
            Assert.AreEqual(result.PaymentResult, PaymentStatus.Success.ToString());

            _paymentRepositoryMock
                .Verify(mock => mock.GetById(It.IsAny<object>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        private GetPaymentDetailsQueryHandler CreateNewGetPaymentDetailsQueryHandler()
        {
            return new GetPaymentDetailsQueryHandler(_mapper,
                _paymentRepositoryMock.Object);
        }
    }
}