using ECommerceAPI.Models;
using ECommerceAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _service;

    public OrdersController(IOrderService service)
    {
        _service = service;
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<IEnumerable<Order>>> GetByUser(int userId)
    {
        return Ok(await _service.GetByUserIdAsync(userId));
    }

    [HttpGet("detail/{id}")]
    public async Task<ActionResult<Order>> GetById(int id)
    {
        var order = await _service.GetByIdAsync(id);
        return order == null ? NotFound() : Ok(order);
    }

    [HttpPost]
    public async Task<ActionResult<Order>> Create(Order order)
    {
        return Ok(await _service.CreateOrderAsync(order));
    }
}
