using ECommerceAPI.Models;
using ECommerceAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;

    public ProductsController(IProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> Get(int id)
    {
        var product = await _service.GetByIdAsync(id);
        return product == null ? NotFound() : Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> Create(Product product)
    {
        var created = await _service.AddAsync(product);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Product product)
    {
        var updated = await _service.UpdateAsync(id, product);
        return updated == null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _service.DeleteAsync(id);
        return success ? NoContent() : NotFound();
    }
}
