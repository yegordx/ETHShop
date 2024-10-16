﻿using ETHShop.Contracts;
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
        var wishList = WishList.Create(Guid.NewGuid(), "Default wishlist").Value;

        user.SetShoppingCart(shoppingCart);
        user.AddWishList(wishList);
        shoppingCart.SetUser(user);
        wishList.SetUser(user);

        await _context.Users.AddAsync(user);
        await _context.ShoppingCarts.AddAsync(shoppingCart);
        await _context.WishLists.AddAsync(wishList);
        await _context.SaveChangesAsync();

        _context.WishLists.Update(wishList);
        _context.ShoppingCarts.Update(shoppingCart);

        await _context.SaveChangesAsync();
    }

    public async Task<User> GetById(Guid Id)
    {
        var userEntity = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.UserId == Id);

        if (userEntity == null)
        {
            throw new InvalidOperationException("User not found");
        }

        return userEntity;
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

    public async Task Update(Guid Id, string UserName, string Email, string HashedPassword, string WalletAddress)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == Id);

        if (user == null)
        {
            throw new InvalidOperationException("User not found");
        }

        user.Update(UserName, Email, HashedPassword, WalletAddress);

        await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid Id)
    {
        var user = await GetById(Id);
        if (user == null)
        {
            throw new InvalidOperationException("User not found");
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task<List<OrderDto>> GetOrders(Guid userID)
    {
        var orders = await _context.Orders
            .Where(o => o.UserID == userID)
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .ToListAsync();

        // Перетворення замовлень в DTO
        var ordersDto = orders.Select(order => new OrderDto(
            order.OrderID,
            order.SellerID,
            order.TotalPriceETH,
            order.OrderItems.Select(oi => new OrderItemDto(
                oi.OrderItemID,
                oi.ProductID,
                oi.Product.ProductName,
                oi.Quantity,
                oi.TotalPrice
            )).ToList(),
            order.OrderDate,
            new ShippingAddressDto(
                    order.ShippingAddress.AddressID,
                    order.ShippingAddress.Name,
                    order.ShippingAddress.Surname,
                    order.ShippingAddress.Country,
                    order.ShippingAddress.City,
                    order.ShippingAddress.AddressLine,
                    order.ShippingAddress.PostalCode
                )
        )).ToList();

        return ordersDto;
    }
}
