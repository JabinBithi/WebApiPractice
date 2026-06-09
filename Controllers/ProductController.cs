using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _service;

    public ProductController(IProductService service)
    {
        _service = service;
    }

    [HttpGet("get")]
    public async Task<IActionResult> GetAll()
    {
        var products = await _service.GetProductsAsync();

        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByRollNo(int id)
    {
        var product = await _service.GetProductByRollNoAsync(id);

        if (product == null)
            return NotFound();

        return Ok(product);
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddProduct([FromBody] ProductInfo product)
    {
        var result = await _service.AddProductAsync(product);

        if (result > 0)
        {
            return Ok(new
            {
                Success = true,
                Message = "Product saved successfully."
            });
        }

        return BadRequest(new
        {
            Success = false,
            Message = "Failed to save product."
        });
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductInfo product)
    {
        if (id != product.Id)
            return BadRequest("ID mismatch.");

        var result = await _service.UpdateProductByIdNoAsync(product);

        if (!result)
            return NotFound();

        return Ok(new
        {
            message = "Product updated successfully"
        });
    }
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var result = await _service.DeleteProductAsync(id);

        if (!result)
            return NotFound(new
            {
                message = $"Product with ID {id} not found."
            });

        return Ok(new
        {
            message = "Product deleted successfully."
        });
    }
}

