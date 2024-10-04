using ETHShop.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ETHShop.Interfaces;

public interface IShoppingCartController
{
    Task<IActionResult> Create(ShoppingCart shoppingCart);
}
