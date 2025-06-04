using ECommerceAPI.Models;
using ECommerceAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly ICartService _service;

    public CartController(ICartService service)
    {
        _service = service;
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<IEnumerable<CartItem>>> GetByUser(int userId)
    {
        return Ok(await _service.GetByUserIdAsync(userId));
    }

    [HttpPost]
    public async Task<ActionResult<CartItem>> Add(CartItem item)
    {
        return Ok(await _service.AddAsync(item));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CartItem>> UpdateQuantity(int id, [FromBody] int quantity)
    {
        var updated = await _service.UpdateQuantityAsync(id, quantity);
        return updated == null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(int id)
    {
        var success = await _service.RemoveAsync(id);
        return success ? NoContent() : NotFound();
    }
}
