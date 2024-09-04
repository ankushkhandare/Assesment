using MediatR;
using Microsoft.AspNetCore.Mvc;
using SampleAPI.Application.Features.Order.Commands;
using SampleAPI.Application.Features.Order.Queries;
using SampleAPI.Entities;

namespace SampleAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }
 
        [HttpGet("recent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<OrderDto>>> GetRecentOrders()
        {
            var recentOrders = await _mediator.Send(new GetRecentOrdersQuery());

            if (recentOrders.Count <= 0)
            {
                return NoContent();
            }
            
            return Ok(recentOrders);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Guid>> CreateOrder([FromBody] CreateOrderCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var orderId = await _mediator.Send(command);
            return Created(String.Empty, orderId);  // Passing uri = Empty string as we don't have GetOrderById Endpoint
        }
    }
}
