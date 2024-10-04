using CSharpFunctionalExtensions;

namespace ETHShop.Entities;

public class ShoppingCart
{
    public ShoppingCart() { }
    private ShoppingCart(Guid id) {
        CartID = id;
        CreatedDate = DateTime.UtcNow;
        CartItems = new HashSet<CartItem>();
    }
    public Guid CartID { get; private set; }

    public Guid UserId { get; private set; }
    public DateTime CreatedDate { get; set; }

    public User User { get; private set; }
    public HashSet<CartItem> CartItems { get; set; }

    public static Result<ShoppingCart> Create(Guid id)
    {
        var shoppingCart = new ShoppingCart(id);
        return Result.Success(shoppingCart);
    }

    public Result SetUser(User user)
    {
        UserId = user.UserId;
        User = user;
        return Result.Success();
    }
}
