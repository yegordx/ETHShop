using ETHShop.PassWordLogic;
using Microsoft.EntityFrameworkCore.Query.Internal;
using ETHShop.Entities;
using ETHShop.Controllers;
using ETHShop.Interfaces;
using ETHShop.Repositories;
using ETHShop.Contracts;

namespace ETHShop.Servieces;

public class CategoryService : ICategoryService
{
    private readonly ICategoriesRepository _categoriesRepository;

    public CategoryService(ICategoriesRepository categoriesRepository)
    {
        _categoriesRepository = categoriesRepository;
    }

    public async Task Add(CreateCategoryRequest request)
    {
        var category = Category.Create(Guid.NewGuid(), request.CategoryName, request.Description);
        await _categoriesRepository.Add(category);
    }
    public async Task<IEnumerable<Category>> GetAll()
    {
        var categorylist = await _categoriesRepository.GetAll();
        
        return categorylist;
    }
    public async Task<Category> GetById(Guid id)
    {
        return await _categoriesRepository.GetById(id);
    }
    public async Task Update(Category category)
    {
        await _categoriesRepository.Update(category);
    }
    public async Task Delete(Guid id)
    {
        await _categoriesRepository.Delete(id);
    }
}
