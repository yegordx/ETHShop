using Microsoft.AspNetCore.Mvc;
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
        // Отримуємо всі категорії, які не видалені
        var categories = await _context.Categories
            .Where(c => !c.isDeleted) // Фільтрація за умовою isDeleted = false
            .Select(c => new
            {
                CategoryID = c.CategoryID,
                CategoryName = c.CategoryName,
                Description = c.Description
            })
            .ToListAsync();

        if (categories == null || categories.Count == 0)
        {
            return NotFound(new { message = "No categories found." });
        }

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

        // Знаходимо категорію
        var category = await _context.Categories
            .Include(c => c.Products) 
            .FirstOrDefaultAsync(c => c.CategoryID == categoryGuid && !c.isDeleted); 

        if (category == null)
        {
            return NotFound(new { message = "Category not found or has been deleted." });
        }

        // Підготовка відповіді
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

        // Знаходимо категорію
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryID == categoryGuid);

        if (category == null)
        {
            return NotFound(new { message = "Category not found." });
        }

        // Оновлюємо назву і опис
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

        // Знаходимо категорію
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryID == categoryGuid);

        if (category == null)
        {
            return NotFound(new { message = "Category not found." });
        }

        // Ставимо isDeleted = true
        category.isDeleted = true;

        _context.Categories.Update(category);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Category marked as deleted." });
    }

    [HttpPut("restore/{categoryId}")]
    public async Task<IActionResult> Restore(string categoryId)
    {
        var categoryGuid = Guid.Parse(categoryId);

        // Знаходимо категорію
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryID == categoryGuid);

        if (category == null)
        {
            return NotFound(new { message = "Category not found." });
        }

        // Відновлюємо категорію
        category.isDeleted = false;

        _context.Categories.Update(category);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Category restored successfully." });
    }
}
