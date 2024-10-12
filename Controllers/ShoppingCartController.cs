using Microsoft.AspNetCore.Mvc;
using ETHShop.Interfaces;
using ETHShop.Entities;
using ETHShop.Contracts;
using ETHShop.Servieces;
using Microsoft.EntityFrameworkCore;
namespace ETHShop.Controllers;

[ApiController]
[Route("api/shoppingcarts")]
public class ShoppingCartController : ControllerBase
{
    private readonly ShopDbContext _context;

    public ShoppingCartController(ShopDbContext context)
    {
        _context = context;
    }

    [HttpPost("{userId}/items")]
    public async Task<IActionResult> AddProductToCart(string userId, AddProductToCartRequest request)
    {
        var userID = Guid.Parse(userId);
        var productID = Guid.Parse(request.ProductID);

        var product = await _context.Products.FirstOrDefaultAsync(u => u.ProductID == productID);
        var shoppingCart = await _context.ShoppingCarts.FirstOrDefaultAsync(u => u.UserId == userID);

        if (product == null || shoppingCart == null)
        {
            return BadRequest(new { message = "Product or shopping cart not found." });
        }

        var cartItem = CartItem.Create(Guid.NewGuid(), shoppingCart, product);
        await _context.CartItems.AddAsync(cartItem);
        await _context.SaveChangesAsync();

        shoppingCart.AddCartItem(cartItem);
        _context.ShoppingCarts.Update(shoppingCart);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Product added to cart successfully." });
    }

    [HttpGet("{userId}/items")]
    public async Task<IActionResult> GetShoppingCartItems(string userId)
    {
        var userID = Guid.Parse(userId);

        var shoppingCart = await _context.ShoppingCarts
            .Include(sc => sc.CartItems)
            .ThenInclude(ci => ci.Product)
            .FirstOrDefaultAsync(sc => sc.UserId == userID);

        if (shoppingCart == null)
        {
            return NotFound(new { message = "Shopping cart not found." });
        }

        var items = shoppingCart.CartItems
            .Select(ci => new CartItemDto(
                ci.CartItemID,
                ci.Product.ProductName,
                ci.Product.PriceETH,
                ci.Quantity)
            ).ToList();

        return Ok(items);
    }

    [HttpDelete("items/{cartItemId}")]
    public async Task<IActionResult> RemoveFromCart(string cartItemId)
    {
        var itemID = Guid.Parse(cartItemId);

        var cartItem = await _context.CartItems.FirstOrDefaultAsync(ci => ci.CartItemID == itemID);
        if (cartItem == null)
        {
            return NotFound(new { message = "Cart item not found." });
        }

        _context.CartItems.Remove(cartItem);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Item removed from cart successfully." });
    }

    [HttpPut("items/{cartItemId}")]
    public async Task<IActionResult> UpdateCartItemQuantity(string cartItemId, bool action)
    {
        var itemId = Guid.Parse(cartItemId);

        var cartItem = await _context.CartItems
            .FirstOrDefaultAsync(ci => ci.CartItemID == itemId);

        if (cartItem == null)
        {
            return NotFound(new { message = "Cart item not found." });
        }

        bool result = cartItem.UpdateQuantity(action);

        if (result)
        {
            _context.CartItems.Update(cartItem);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Cart item quantity updated successfully." });
        }
        return BadRequest();
    }

    [HttpGet("items/{cartItemId}")]
    public async Task<IActionResult> GetByID(string cartItemId)
    {
        var itemId = Guid.Parse(cartItemId);

        var cartItem = await _context.CartItems
            .Include(ci => ci.Product)
            .FirstOrDefaultAsync(ci => ci.CartItemID == itemId);

        if (cartItem == null)
        {
            return NotFound(new { message = "Cart item not found." });
        }

        var response = new CartItemDto(
            cartItem.CartItemID,
            cartItem.Product.ProductName,
            cartItem.Product.PriceETH,
            cartItem.Quantity
        );

        return Ok(response);
    }
}

