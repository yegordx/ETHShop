using ETHShop.Entities;
using ETHShop.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ETHShop.Repositories;

public class CategoriesRepository : ICategoriesRepository
{
    private readonly ShopDbContext _context;
    public CategoriesRepository(ShopDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Category>> GetAll()
    {
        var categorylist = await _context.Categories.ToListAsync();
        return categorylist;
    }
    public async Task<Category> GetById(Guid id)
    {
        var categoryEntity = await _context.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.CategoryID == id) ?? throw new Exception();

        return categoryEntity;
    }
    public async Task Add(Category category)
    {
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
    }
    public async Task Update(Category category)
    {
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
    }
    public async Task Delete(Guid id)
    {
        var category = await GetById(id);
        if (category != null)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}
