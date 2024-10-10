using Microsoft.AspNetCore.Mvc;
using ETHShop.Entities;
using ETHShop.Servieces;
using ETHShop.Contracts;
using ETHShop.Interfaces;
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
        var userId = Guid.Parse(UserId);
        var wishLists = _context.WishLists
            .Include(w => w.WishListItems)
            .Where(w=>w.UserID == userId);

        return Ok(wishLists);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateWishListRequest request)
    {
        var userId = Guid.Parse(request.UserId);
        var wishList = WishList.Create(Guid.NewGuid(), request.WishListName).Value;
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.UserId == userId);
        wishList.SetUser(user);

        _context.WishLists.Add(wishList);

        user.AddWishList(wishList);

        _context.Users.Update(user);
        await _context.SaveChangesAsync(); 
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> AddProductToWishList(string UserId, string ProductId)
    {

        return Ok();
    }
}
