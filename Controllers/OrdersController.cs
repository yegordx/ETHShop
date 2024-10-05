using Microsoft.AspNetCore.Mvc;

namespace ETHShop.Controllers;

public class OrdersController : ControllerBase
{
    private readonly ShopDbContext _context;

    public OrdersController(ShopDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> MakeOrder()
    {
        return Ok();
    }
}
