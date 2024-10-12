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

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUsersWishLists(string userId)
    {
        try
        {
            var userIdGuid = Guid.Parse(userId);
            var wishLists = await _context.WishLists
                .Include(w => w.WishListItems)
                .ThenInclude(wi => wi.Product)
                .Where(w => w.UserID == userIdGuid)
                .ToListAsync();

            var wishListsDto = wishLists.Select(w => new WishListDto(w.WishListID, w.Name, 
                w.WishListItems.Select(wi => new WishListItemDto(
                    wi.WishListItemID, 
                    wi.ProductID, 
                    wi.Product.ProductName, 
                    wi.DateAdded))
                .ToList()))
                .ToList();

            return Ok(wishListsDto);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost]
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

    [HttpPost("{wishlistId}/products")]
    public async Task<IActionResult> AddProductToWishList(AddProductToWishListRequest request)
    {
        var wishlistId = Guid.Parse(request.WishListID);
        var productID = Guid.Parse(request.ProductID);

        var wishList = await _context.WishLists
            .FirstOrDefaultAsync(w => w.WishListID == wishlistId);

        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.ProductID == productID);

        if (product == null)
        {
            return NotFound(new { message = "Product not found." });
        }

        if (wishList == null)
        {
            return NotFound(new { message = "WishList not found." });
        }

        var wishItem = wishList.AddItem(product);
        _context.WishListItems.Add(wishItem);
        _context.WishLists.Update(wishList);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWishList(string id)
    {
        var wishId = Guid.Parse(id);
        var wishList = await _context.WishLists.FirstOrDefaultAsync(w => w.WishListID == wishId);
        if (wishList == null)
        {
            return BadRequest();
        }

        _context.WishLists.Remove(wishList);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("remove/{id}")]
    public async Task<IActionResult> RemoveFromWishList(string id)
    {
        var wiId = Guid.Parse(id);
        var wishListItem = await _context.WishListItems.FirstOrDefaultAsync(wi => wi.WishListItemID == wiId);
        if (wishListItem == null)
        {
            return BadRequest();
        }
        _context.WishListItems.Remove(wishListItem);
        await _context.SaveChangesAsync();
        return Ok();
    }
}