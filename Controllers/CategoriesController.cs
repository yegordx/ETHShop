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
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<GetCategoriesResponse> GetAll()
    {
        var categories = await _categoryService.GetAll();

        var categoriesDto = categories
            .Select(c => new CategoryDto(c.CategoryID, c.CategoryName, c.Description))
            .ToList(); 

        GetCategoriesResponse response = new GetCategoriesResponse(categoriesDto);
        return response;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryRequest request)
    {
        await _categoryService.Add(request);
        return Ok();
    }

    
}
