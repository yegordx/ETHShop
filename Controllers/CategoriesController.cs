﻿using Microsoft.AspNetCore.Mvc;
using ETHShop.Entities;
using ETHShop.Servieces;
using ETHShop.Contracts;
using ETHShop.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ETHShop.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoriesController : ControllerBase
{
    private readonly ShopDbContext _context;

    public CategoriesController(ShopDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var categories = await _context.Categories
            .Where(c => !c.isDeleted)
            .Select(c => new CategoryDto(c.CategoryID, c.CategoryName, c.Description))
            .ToListAsync();

        return Ok(categories);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryRequest request)
    {
        var category = Category.Create(Guid.NewGuid(), request.CategoryName, request.Description);
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpGet("{categoryId}")]
    public async Task<IActionResult> Details(string categoryId)
    {
        var categoryGuid = Guid.Parse(categoryId);

        var category = await _context.Categories
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.CategoryID == categoryGuid && !c.isDeleted);

        if (category == null)
        {
            return NotFound(new { message = "Category not found or has been deleted." });
        }

        var response = new
        {
            CategoryName = category.CategoryName,
            Description = category.Description,
            Products = category.Products.Select(p => new
            {
                p.ProductName,
                p.Description,
                p.PriceETH
            }).ToList()
        };

        return Ok(response);
    }

    [HttpPut("{categoryId}")]
    public async Task<IActionResult> Update(string categoryId, [FromBody] UpdateCategoryRequest request)
    {
        var categoryGuid = Guid.Parse(categoryId);

        var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryID == categoryGuid);

        if (category == null)
        {
            return NotFound(new { message = "Category not found." });
        }

        category.CategoryName = request.CategoryName;
        category.Description = request.Description;

        _context.Categories.Update(category);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Category updated successfully." });
    }

    [HttpDelete("{categoryId}")]
    public async Task<IActionResult> Delete(string categoryId)
    {
        var categoryGuid = Guid.Parse(categoryId);

        var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryID == categoryGuid);

        if (category == null)
        {
            return NotFound(new { message = "Category not found." });
        }

        category.isDeleted = true;

        _context.Categories.Update(category);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Category marked as deleted." });
    }

    [HttpPut("{categoryId}/restore")]
    public async Task<IActionResult> Restore(string categoryId)
    {
        var categoryGuid = Guid.Parse(categoryId);

        var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryID == categoryGuid);

        if (category == null)
        {
            return NotFound(new { message = "Category not found." });
        }

        category.isDeleted = false;

        _context.Categories.Update(category);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Category restored successfully." });
    }
}
