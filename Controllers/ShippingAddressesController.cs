using Microsoft.AspNetCore.Mvc;
using ETHShop.Entities;
using ETHShop.Servieces;
using ETHShop.Contracts;
using ETHShop.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ETHShop.Controllers;

[ApiController]
[Route("api/addresses")]
public class ShippingAddressesController : ControllerBase
{
    private readonly ShopDbContext _context;

    public ShippingAddressesController (ShopDbContext context)
    {
        _context = context;
    }

    [HttpPost("{userId}")]
    public async Task<IActionResult> AddShippingAddress(AddShippingAddressRequest request)
    {
        var userId = Guid.Parse(request.UserId);
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.UserId == userId);

        var address = new ShippingAddress(Guid.NewGuid(), user, request.Name, request.Surname, request.Country, request.City, request.AddressLine, request.PostalCode);
        user.AddAddress(address);

        _context.ShippingAddresses.Add(address);
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPut("{addressId}")]
    public async Task<IActionResult> UpdateShippingAddress(Guid addressId, AddShippingAddressRequest request)
    {
        var address = await _context.ShippingAddresses
            .FirstOrDefaultAsync(a => a.AddressID == addressId);

        if (address == null)
        {
            return NotFound(new { message = "Address not found." });
        }

        address.Name = request.Name;
        address.Surname = request.Surname;
        address.Country = request.Country;
        address.City = request.City;
        address.AddressLine = request.AddressLine;
        address.PostalCode = request.PostalCode;

        _context.ShippingAddresses.Update(address);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Address updated successfully." });
    }

    [HttpDelete("{addressId}")]
    public async Task<IActionResult> DeleteShippingAddress(Guid addressId)
    {
        var address = await _context.ShippingAddresses
            .FirstOrDefaultAsync(a => a.AddressID == addressId);

        if (address == null)
        {
            return NotFound(new { message = "Address not found." });
        }

        _context.ShippingAddresses.Remove(address);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Address deleted successfully." });
    }

    [HttpGet("{userId}/all")]
    public async Task<IActionResult> GetShippingAddresses(string userId)
    {
        var userIdGuid = Guid.Parse(userId);

        var addresses = await _context.ShippingAddresses
            .Where(a => a.User.UserId == userIdGuid)
            .Select(a => new ShippingAddressDto (a.AddressID, a.Name, a.Surname, a.Country, a.City, a.AddressLine, a.PostalCode))
            .ToListAsync();

        return Ok(addresses);
    }
}
