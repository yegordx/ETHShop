using CSharpFunctionalExtensions;

namespace ETHShop.Entities;

public class CartItem
{
    public CartItem() { }
    private CartItem(Guid id, Guid cartId, Guid productID, int quantity, DateTime dateAdded, ShoppingCart shoppingCart, Product product) {
        CartItemID = id;
        CartID = cartId;
        ProductID = productID;
        Quantity = quantity;
        DateAdded = dateAdded;
        ShoppingCart = shoppingCart;
        Product = product;
    }
    public Guid CartItemID { get; }
    public Guid CartID { get; }
    public Guid ProductID { get;}
    public int Quantity { get; }
    public DateTime DateAdded { get; }

    // Навігаційні властивості
    public ShoppingCart ShoppingCart { get; }
    public Product Product { get;  }

    public static Result<CartItem> Create(Guid id, Guid cartId, Guid productID, int quantity, DateTime dateAdded, ShoppingCart shoppingCart, Product product)
    {
        var cartItem = new CartItem(id, cartId, productID, quantity, dateAdded, shoppingCart, product);

        return Result.Success<CartItem>(cartItem);
    }
}
