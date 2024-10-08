﻿using Microsoft.AspNetCore.Mvc;
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
            return Ok(new { message = "Product added successfully." });
        }

        
        return StatusCode(500, new { message = "An error occurred while adding the product." });
    }

    [HttpGet]
    public async Task<GetProductsResponse> GetAll()
    {
        var products = await _productsService.GetAllAsync();

        var productsDto = products
            .Select(c => new ProductDto(c.ProductID, c.ProductName, c.Description, c.PriceETH))
            .ToList();

        var response = new GetProductsResponse(productsDto);
        return response;
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

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _productsService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound(new { message = "Product not found." });
        }

        return Ok(new { message = "Product deleted successfully." });
    }
}

