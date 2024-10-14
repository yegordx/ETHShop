using Microsoft.AspNetCore.Mvc;
using ETHShop.Entities;
using ETHShop.Servieces;
using ETHShop.Contracts;
using ETHShop.Interfaces;

namespace ETHShop.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterUserRequest request)
    {
        if (!EmailChecker.IsValidEmail(request.Email))
        {
            return BadRequest("Invalid email format.");
        }
        try
        {
            var token = await _userService.Register(Guid.NewGuid(), request.UserName, request.Password, request.Email, request.WalletAddress);
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddHours(24)
            };

            return Ok(new { token });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = "User already exists: " + ex.Message });
        }
    }

    [HttpPost("/login")]
    public async Task<IActionResult> Login(LoginUserRequest request)
    {
        try
        {
            var token = await _userService.Login(request.Email, request.Password);
            return Ok(new { token });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("{userId}/orders")]
    public async Task<IActionResult> GetOrders(Guid userId)
    {
        var orders = await _userService.GetOrders(userId);
        return Ok(orders);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var entityList = await _userService.GetAll();
        return Ok(entityList);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserRequest request)
    {
        if (!EmailChecker.IsValidEmail(request.Email))
        {
            return BadRequest("Invalid email format.");
        }
        await _userService.Update(id, request.UserName, request.Email, request.Password, request.WalletAddress);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        await _userService.Delete(id);
        return NoContent();
    }
}

