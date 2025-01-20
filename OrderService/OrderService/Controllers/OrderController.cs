using MassTransit;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Contracts;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IBus _bus;

        public OrderController(IBus bus)
        {
            _bus = bus;
        }

        // POST: api/orders
        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] OrderRequest orderRequest)
        {
            if (orderRequest == null)
                return BadRequest("Invalid order data.");

            // Create OrderPlacedEvent
            var orderPlacedEvent = new OrderPlacedEvent(
                Guid.NewGuid(),
                orderRequest.CustomerEmail,
                orderRequest.TotalPrice
            );

            // Publish the event to MassTransit (which will route it to Orchestrator Service)
            await _bus.Publish(orderPlacedEvent);

            return Ok(new { Message = "Order placed successfully.", OrderId = orderPlacedEvent.OrderId });
        }
    }

    public class OrderRequest
    {
        public string CustomerEmail { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
