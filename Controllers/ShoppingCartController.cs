using Microsoft.AspNetCore.Mvc;
using ETHShop.Interfaces;
using ETHShop.Entities;
namespace ETHShop.Controllers;

[ApiController]
[Route("api/shoppingcarts")]
public class ShoppingCartController : ControllerBase, IShoppingCartController
{
    private readonly ShopDbContext _context;

    public ShoppingCartController(ShopDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Create(ShoppingCart shoppingCart)
    {
        //Guid id, Guid userId, DateTime createdAt
        _context.ShoppingCarts.Add(shoppingCart);
        await _context.SaveChangesAsync();
        return Ok(shoppingCart);
    }
}
