using ETHShop.Controllers;
using ETHShop.Entities;
using ETHShop.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ETHShop.Repositories;

public class UserRepository : IUsersRepository
{
    private readonly ShopDbContext _context;
    public UserRepository(ShopDbContext context)
    {
        _context = context;
    }

    public async Task Add(User user)
    {
        var existingUser = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == user.Email || u.UserName == user.UserName);

        if (existingUser != null)
        {
            throw new InvalidOperationException("User with the same email or username already exists.");
        }

        var shoppingCart = ShoppingCart.Create(Guid.NewGuid()).Value;

        // Асоціюємо кошик із користувачем
        user.SetShoppingCart(shoppingCart);
        shoppingCart.SetUser(user);

        // Додаємо користувача і кошик до контексту
        await _context.Users.AddAsync(user);
        await _context.ShoppingCarts.AddAsync(shoppingCart);
        await _context.SaveChangesAsync();

        _context.Users.Update(user);
        _context.ShoppingCarts.Update(shoppingCart);

        await _context.SaveChangesAsync();
    }

    public async Task<User> GetByEmail(string email)
    {
        var userEntity = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email);

        if (userEntity == null)
        {
            throw new InvalidOperationException("User not found");
        }

        return userEntity;
    }


    public async Task<IEnumerable<User>> GetAll()
    {
        var userlist = await _context.Users.ToListAsync();
        return userlist;
    }

    public async Task Update(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(string email)
    {
        var user = await GetByEmail(email);
        if (user == null)
        {
            throw new InvalidOperationException("User not found");
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}
