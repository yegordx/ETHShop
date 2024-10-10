﻿using ETHShop.Contracts;
using ETHShop.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ETHShop.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly ShopDbContext _context;

    public OrdersController(ShopDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> MakeOrder(CreateOrdetRequest request)
    {
        var userId = Guid.Parse(request.UserId);

        // Отримуємо користувача та його кошик
        var user = await _context.Users
            .Include(x => x.ShoppingCart)
            .ThenInclude(cart => cart.CartItems)
            .ThenInclude(items => items.Product)
            .ThenInclude(prod => prod.Seller)
            .FirstOrDefaultAsync(u => u.UserId == userId);

        if (user == null)
        {
            return BadRequest("User not found");
        }

        var cartItems = user.ShoppingCart.CartItems
            .Where(item => request.ItemsId.Contains(item.CartItemID.ToString()))
            .ToList();

        if (cartItems.Count == 0)
        {
            return BadRequest("No valid items in the shopping cart");
        }

        // Перевіряємо, що всі товари мають одного продавця
        var sellerId = cartItems.First().Product.SellerID;
        if (!cartItems.All(item => item.Product.Seller.SellerID == sellerId))
        {
            return BadRequest("All products must have the same seller");
        }

        var seller = await _context.Sellers
            .Include(s => s.Orders)
            .FirstOrDefaultAsync(s => s.SellerID == sellerId);

        if (seller == null)
        {
            return BadRequest("Seller not found");
        }

        var order = new Order(Guid.NewGuid(), userId, sellerId);

        await _context.Orders.AddAsync(order);
        List<OrderItem> orderItems = order.AddOrderItems(cartItems);
        await _context.OrderItems.AddRangeAsync(orderItems);
        await _context.SaveChangesAsync();


        user.Orders.Add(order);
        seller.Orders.Add(order);
        _context.Users.Update(user);
        _context.Sellers.Update(seller);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders(string userID)
    {
        var userId = Guid.Parse(userID);

        // Отримуємо користувача разом із замовленнями і товарами в них
        var user = await _context.Users
            .Include(u => u.Orders)
            .ThenInclude(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(u => u.UserId == userId);

        if (user == null)
        {
            return NotFound("User not found");
        }

        // Проекція до OrderDto
        var ordersDto = user.Orders.Select(order => new OrderDto(
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
            order.OrderDate
        )).ToList();

        return Ok(ordersDto);
    }

}
