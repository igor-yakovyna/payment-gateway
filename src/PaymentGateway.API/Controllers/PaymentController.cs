using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Application.Commands;
using PaymentGateway.Application.Queries;

namespace PaymentGateway.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly IMediator _mediator;

        public PaymentController(ILogger<PaymentController> logger, IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PaymentDetailsViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetPayment([FromRoute] Guid id)
        {
            _logger.LogInformation($"Request to GetPayment: {id}");

            var query = new GetPaymentDetailsQuery(id);
            var payment = await _mediator.Send(query);

            if (payment is null)
            {
                return NotFound(id);
            }

            return Ok(payment);
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ProcessPayment([FromBody] ProcessPaymentCommand command)
        {
            _logger.LogInformation("Request to ProcessPayment.");

            await _mediator.Send(command);

            return NoContent();
        }
    }
}