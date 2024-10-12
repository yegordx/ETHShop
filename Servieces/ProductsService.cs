using ETHShop.PassWordLogic;
using Microsoft.EntityFrameworkCore.Query.Internal;
using ETHShop.Entities;
using ETHShop.Controllers;
using ETHShop.Interfaces;
using ETHShop.Repositories;
using ETHShop.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ETHShop.Servieces;

public class ProductsService : IProductsService
{
    private readonly ShopDbContext _context;

    public ProductsService(ShopDbContext context)
    {
        _context = context;
    }


    public async Task<bool> AddAsync(Product product, Guid SellerID, string CategoryName)
    {
        try
        {
            var seller = await _context.Sellers.FirstOrDefaultAsync(s => s.SellerID == SellerID);
            if (seller == null)
            {
                throw new InvalidOperationException($"Seller with ID {SellerID} not found.");
            }

            var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryName == CategoryName);
            if (category == null)
            {
                throw new InvalidOperationException($"Category with name {CategoryName} not found.");
            }
            product.SetCategory(category);
            product.SetSeller(seller);

            
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            seller.AddProduct(product);
            category.AddProduct(product);

            _context.Sellers.Update(seller);
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();


            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }

    // Отримати продукт за ID
    public async Task<Product> GetByIdAsync(Guid id)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.ProductID == id);
    }

    // Редагувати існуючий продукт
    public async Task<bool> EditAsync(Product product)
    {
        var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.ProductID == product.ProductID);
        if (existingProduct == null)
        {
            return false;
        }

        // Оновлюємо властивості продукту
        existingProduct.ProductName = product.ProductName;
        existingProduct.Description = product.Description;
        existingProduct.PriceETH = product.PriceETH;

        _context.Products.Update(existingProduct);
        await _context.SaveChangesAsync();
        return true;
    }

    // Видалити продукт за ID
    public async Task<bool> DeleteAsync(Guid SellerId, Guid ProductId)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductID == ProductId);
        if (product == null||product.SellerID != SellerId)
        {
            return false;
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
        
    }

}
