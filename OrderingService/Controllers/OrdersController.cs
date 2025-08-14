using Contracts.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using OrderingService.Data;
using OrderingService.Models;

namespace OrderingService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly OrderContext _db;
    private readonly IPublishEndpoint _publish;

    public OrdersController(OrderContext db, IPublishEndpoint publish)
    {
        _db = db;
        _publish = publish;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderDto dto)
    {
        var order = new Order
        {
            CustomerId = dto.CustomerId,
            Total = dto.Total,
            CreatedAt = DateTime.UtcNow
        };

        _db.Orders.Add(order);
        await _db.SaveChangesAsync(); // önce DB

        await _publish.Publish<OrderCreated>(new   // sonra publish
        {
            OrderId = order.Id,
            order.CustomerId,
            order.Total,
            order.CreatedAt
        });

        return CreatedAtAction(nameof(Get), new { id = order.Id }, order);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var o = await _db.Orders.FindAsync(id);
        return o is null ? NotFound() : Ok(o);
    }
}

public record CreateOrderDto(int CustomerId, decimal Total);
