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

    [HttpGet("{rollNo}")]
    public async Task<IActionResult> GetByRollNo(string rollNo)
    {
        var product = await _service.GetProductByRollNoAsync(rollNo);

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
}

