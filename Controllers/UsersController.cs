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

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserRequest request)
    {
        if (!EmailChecker.IsValidEmail(request.Email)) {
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

            //Response.Cookies.Append("jwt", token, cookieOptions);
            return Ok(new { token });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = "User already exists: " + ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserRequest request)
    {
        try
        {
            var token = await _userService.Login(request);

            

            return Ok(new {token });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var entityList = await _userService.GetAll();
        return Ok(entityList);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] User user)
    {
        if (id != user.UserId)
        {
            return BadRequest();
        }
        await _userService.Update(user);
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUser(string email)
    {
        await _userService.Delete(email);
        return NoContent();
    }
}
