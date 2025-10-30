using Microsoft.AspNetCore.Mvc;
using FastPoint.Application.Services;
using FastPoint.Domain.Entities;

namespace FastPoint.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService _service;

        public OrdersController(OrderService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var order = await _service.GetOrderAsync(id);
            return order == null ? NotFound() : Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Guid userId, [FromBody] List<OrderItemRequest> items)
        {
            var order = await _service.CreateOrderAsync(
                userId,
                items.Select(i => (i.ProductId, i.Quantity)).ToList()
            );
            return CreatedAtAction(nameof(Get), new { id = order.Id }, order);
        }

        public record OrderItemRequest(Guid ProductId, int Quantity);
    }
}
