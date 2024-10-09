using Microsoft.AspNetCore.Mvc;
using ETHShop.Entities;
using ETHShop.Servieces;
using ETHShop.Contracts;
using ETHShop.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ETHShop.Controllers;

[ApiController]
[Route("api/sellers")]
public class SellersController : ControllerBase
{ 
    private readonly ISellersService _sellersService;

    public SellersController(ISellersService sellersService)
    {
        _sellersService = sellersService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginSellerRequest request)
    {
        try
        {
            var userId = Guid.Parse(request.UserID);
            var token = await _sellersService.Login(userId);
            return Ok(new { Token = token });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterSellerRequest request)
    {
        if (!EmailChecker.IsValidEmail(request.ContactEmail))
        {
            return BadRequest(new { message = "Invalid email format." });
        }

        try
        {
            var userId = Guid.Parse(request.UserID);
            var token = await _sellersService.Register(userId, request.StoreName, request.StoreDescription, request.ContactEmail, request.ContactPhone);
            return Ok(new { Token = token });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var sellers = await _sellersService.GetAllAsync();
        return Ok(sellers);
    }

    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var seller = await _sellersService.GetByIdAsync(id);
            return Ok(seller);
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }


    [HttpGet("getOrders/")]
    public async Task<IActionResult> GetOrders(string sellerID)
    {
        var sellerId = Guid.Parse(sellerID);

        var orders = await _sellersService.GetOrders(sellerId);

        return Ok(orders);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateSellerRequest request)
    {
        try
        {
            var seller = new Seller
            {
                SellerID = id,
                StoreName = request.StoreName,
                StoreDescription = request.StoreDescription,
                ContactEmail = request.ContactEmail,
                ContactPhone = request.ContactPhone
            };

            await _sellersService.UpdateAsync(seller);
            return Ok(new { message = "Seller updated successfully." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _sellersService.DeleteAsync(id);
            return Ok(new { message = "Seller deleted successfully." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
