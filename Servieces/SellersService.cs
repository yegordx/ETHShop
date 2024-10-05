using ETHShop.Entities;
using ETHShop.Controllers;
using ETHShop.Interfaces;
using ETHShop.Repositories;
using ETHShop.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ETHShop.Servieces;

public class SellersService : ISellersService
{
    private readonly ShopDbContext _context;
    private readonly IJwtProvider _jwtProvider;

    public SellersService(ShopDbContext context, IJwtProvider provider)
    {
        _context = context;
        _jwtProvider = provider;
    }

    public async Task<string> Register(Guid UserID, string StoreName, string StoreDescription, string ContactEmail, string ContactPhone)
    {
        var seller = Seller.Create(Guid.NewGuid(), StoreName, StoreDescription, ContactEmail, ContactPhone);
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.UserId == UserID);

        user.SetSeller(seller);
        seller.SetUser(user);

        await _context.Sellers.AddAsync(seller);
        await _context.SaveChangesAsync();
        _context.Users.Update(user);

        await _context.SaveChangesAsync();

        var token = _jwtProvider.GenerateSellerToken(seller);
        return token;
    }

    public async Task<string> Login(Guid UserID)
    {
        var user = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.UserId == UserID);

        if (user.Seller == null)
        {
            throw new InvalidOperationException("User does not have a seller account");
        }

        var token = _jwtProvider.GenerateSellerToken(user.Seller);
        return token;
    }

    // Метод для отримання всіх продавців (Read)
    public async Task<IEnumerable<Seller>> GetAllAsync()
    {
        return await _context.Sellers
            .AsNoTracking()
            .ToListAsync();
    }

    // Метод для отримання продавця за ID (Read)
    public async Task<Seller> GetByIdAsync(Guid SellerID)
    {
        var seller = await _context.Sellers
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.SellerID == SellerID);

        if (seller == null)
        {
            throw new InvalidOperationException("Seller not found");
        }

        return seller;
    }

    // Метод для оновлення інформації про продавця (Update)
    public async Task UpdateAsync(Seller seller)
    {
        var existingSeller = await _context.Sellers
            .FirstOrDefaultAsync(s => s.SellerID == seller.SellerID);

        if (existingSeller == null)
        {
            throw new InvalidOperationException("Seller not found");
        }

        existingSeller.StoreName = seller.StoreName;
        existingSeller.StoreDescription = seller.StoreDescription;
        existingSeller.ContactEmail = seller.ContactEmail;
        existingSeller.ContactPhone = seller.ContactPhone;

        _context.Sellers.Update(existingSeller);
        await _context.SaveChangesAsync();
    }

    // Метод для видалення продавця за ID (Delete)
    public async Task DeleteAsync(Guid SellerID)
    {
        var seller = await _context.Sellers
            .FirstOrDefaultAsync(s => s.SellerID == SellerID);

        if (seller == null)
        {
            throw new InvalidOperationException("Seller not found");
        }

        _context.Sellers.Remove(seller);
        await _context.SaveChangesAsync();
    }
}

