using Microsoft.AspNetCore.Mvc;
using ETHShop.Entities;
using ETHShop.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ETHShop.Controllers;

[ApiController]
[Route("api/wishlists")]
public class WishListController : ControllerBase
{
    private readonly ShopDbContext _context;

    public WishListController(ShopDbContext context)
    {
        _context = context;
    }

    [HttpGet("getUsersWishLists")]
    public async Task<IActionResult> GetUsersWishLists(string UserId)
    {
        try
        {
            var userId = Guid.Parse(UserId);
            var wishLists = await _context.WishLists
                .Include(w => w.WishListItems)
                .Where(w => w.UserID == userId)
                .ToListAsync();

            return Ok(wishLists);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateWishListRequest request)
    {
        try
        {
            var userId = Guid.Parse(request.UserId);
            var wishList = WishList.Create(Guid.NewGuid(), request.WishListName).Value;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            wishList.SetUser(user);
            _context.WishLists.Add(wishList);
            user.AddWishList(wishList);
            _context.Users.Update(user);

            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("add-product")]
    public async Task<IActionResult> AddProductToWishList(string UserId, string ProductId)
    {
        // Логіка для додавання продукту до списку бажань
        return Ok();
    }
}