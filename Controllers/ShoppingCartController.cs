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

    [HttpPost("AddToCart")]
    public async Task<IActionResult> AddProductToCart(AddProductToCartRequest request)
    {
        var userID = Guid.Parse(request.UserID);
        var productID = Guid.Parse(request.ProductID);

        var product = await _context.Products
            .FirstOrDefaultAsync(u => u.ProductID == productID);



        var shoppingCart = await _context.ShoppingCarts
           .FirstOrDefaultAsync(u => u.UserId == userID);

        if (product == null || shoppingCart == null)
        {
            return BadRequest();
        }

        var cartItem = CartItem.Create(Guid.NewGuid(), shoppingCart, product);

        await _context.CartItems.AddAsync(cartItem);

        await _context.SaveChangesAsync();

        shoppingCart.AddCartItem(cartItem);

        _context.ShoppingCarts.Update(shoppingCart);

        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetShoppingCartItems(string UserID)
    {
        // Конвертація UserID зі string у Guid
        var userID = Guid.Parse(UserID);

        // Отримання ShoppingCart разом із CartItems
        var shoppingCart = await _context.ShoppingCarts
            .Include(sc => sc.CartItems)
            .ThenInclude(ci => ci.Product) // Завантажуємо продукт для кожного CartItem
            .FirstOrDefaultAsync(sc => sc.UserId == userID);

        // Перевірка, чи знайдено ShoppingCart
        if (shoppingCart == null)
        {
            return NotFound(new { message = "Shopping cart not found." });
        }

        // Формуємо список елементів у кошику
        var items = shoppingCart.CartItems
            .Select(ci => new CartItemDto(
                ci.CartItemID,
                ci.Product.ProductName,
                ci.Product.PriceETH,
                ci.Quantity)
            ).ToList();

        return Ok(items);
    }

    [HttpDelete("{cartItemId}")]
    public async Task<IActionResult> RemoveFromCart(string cartItemId)
    {
        try
        {
            var itemID = Guid.Parse(cartItemId);

            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.CartItemID == itemID);

            if (cartItem == null)
            {
                return NotFound(new { message = "Cart item not found." });
            }

            // Видаляємо товар із кошика
            _context.CartItems.Remove(cartItem);

            // Зберігаємо зміни в базі даних
            await _context.SaveChangesAsync();

            // Повертаємо відповідь про успішне видалення
            return Ok(new { message = "Item removed from cart successfully." });
        }
        catch (Exception ex)
        {
            // Обробляємо можливі помилки
            return BadRequest(new { message = $"An error occurred: {ex.Message}" });
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCartItemQuantity(string cartItemId, string action)
    {
        var itemId = Guid.Parse(cartItemId);

        // Знайти товар у кошику за ID
        var cartItem = await _context.CartItems
            .FirstOrDefaultAsync(ci => ci.CartItemID == itemId);

        if (cartItem == null)
        {
            return NotFound(new { message = "Cart item not found." });
        }

        // Залежно від параметра 'action' збільшуємо або зменшуємо кількість
        bool result = cartItem.UpdateQuantity(action);

        if (result)
        {
            _context.CartItems.Update(cartItem);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Cart item quantity updated successfully." });
        }
        return BadRequest();
    }

    [HttpGet("{cartItemId}")]
    public async Task<IActionResult> GetByID(string cartItemId)
    {
        var itemId = Guid.Parse(cartItemId);

        // Знайти товар у кошику за ID
        var cartItem = await _context.CartItems
            .Include(ci => ci.Product) // Опціонально, якщо треба отримати дані продукту
            .FirstOrDefaultAsync(ci => ci.CartItemID == itemId);

        if (cartItem == null)
        {
            return NotFound(new { message = "Cart item not found." });
        }

        // Підготовка даних для відповіді (опціонально, можна повертати весь об'єкт)
        var response = new CartItemDto(
            cartItem.CartItemID, 
            cartItem.Product.ProductName, 
            cartItem.Product.PriceETH, 
            cartItem.Quantity
            );

        return Ok(response);
    }
}
