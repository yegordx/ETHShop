using Microsoft.AspNetCore.Mvc;
using ETHShop.Entities;
using ETHShop.Contracts;
using ETHShop.Interfaces;

namespace ETHShop.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly IProductsService _productsService;

    public ProductController(IProductsService productsService)
    {
        _productsService = productsService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductRequest request)
    {
        if (request == null || string.IsNullOrEmpty(request.ProductName) || request.PriceETH <= 0)
        {
            return BadRequest(new { message = "Invalid product data." });
        }

        var product = new Product
        {
            ProductID = Guid.NewGuid(),
            ProductName = request.ProductName,
            Description = request.Description,
            PriceETH = request.PriceETH
        };

        var sellerId = Guid.Parse(request.SellerID);

        var result = await _productsService.AddAsync(product, sellerId, request.CategoryName);

        if (result)
        {
            return CreatedAtAction(nameof(GetById), new { id = product.ProductID }, new { message = "Product added successfully.", productId = product.ProductID });
        }

        return StatusCode(500, new { message = "An error occurred while adding the product." });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productsService.GetAllAsync();

        if (products == null || !products.Any())
        {
            return NotFound(new { message = "No products found." });
        }

        var productsDto = products
            .Select(c => new ProductDto(c.ProductID, c.ProductName, c.Description, c.PriceETH))
            .ToList();

        return Ok(new GetProductsResponse(productsDto));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var product = await _productsService.GetByIdAsync(id);

        if (product == null)
        {
            return NotFound(new { message = "Product not found." });
        }

        var productDto = new ProductDto(product.ProductID, product.ProductName, product.Description, product.PriceETH);
        return Ok(productDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(Guid id, UpdateProductRequest request)
    {
        if (request == null || string.IsNullOrEmpty(request.ProductName) || request.PriceETH <= 0)
        {
            return BadRequest(new { message = "Invalid product data." });
        }

        var product = await _productsService.GetByIdAsync(id);

        if (product == null)
        {
            return NotFound(new { message = "Product not found." });
        }

        product.ProductName = request.ProductName;
        product.Description = request.Description;
        product.PriceETH = request.PriceETH;

        var result = await _productsService.UpdateAsync(product);

        if (result)
        {
            return Ok(new { message = "Product updated successfully." });
        }

        return StatusCode(500, new { message = "An error occurred while updating the product." });
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] Guid SellerId, [FromQuery] Guid ProductId)
    {
        var deleted = await _productsService.DeleteAsync(SellerId, ProductId);

        if (!deleted)
        {
            return NotFound(new { message = "Product not found." });
        }

        return Ok(new { message = "Product deleted successfully." });
    }
}


