﻿using CSharpFunctionalExtensions;

namespace ETHShop.Entities;

public class CartItem
{
    public CartItem() { }
    private CartItem(Guid id, ShoppingCart shoppingCart, Product product) {
        CartItemID = id;
        CartID = shoppingCart.CartID;
        ProductID = product.ProductID;
        Quantity = 1;
        DateAdded = DateTime.UtcNow;
        ShoppingCart = shoppingCart;
        Product = product;
    }
    public Guid CartItemID { get; }
    public Guid CartID { get; }
    public Guid ProductID { get;}
    public int Quantity { get; set; }
    public DateTime DateAdded { get; }

    // Навігаційні властивості
    public ShoppingCart ShoppingCart { get; }
    public Product Product { get;  }

    public static CartItem Create(Guid id, ShoppingCart shoppingCart, Product product)
    {
        var cartItem = new CartItem(id, shoppingCart, product);

        return cartItem;
    }

    public bool UpdateQuantity(bool action)
    {
        if (action == true)
        {
            Quantity += 1;
            return true;
        }
        else if (action == false)
        {
            if (Quantity > 1)
            {
                Quantity -= 1;
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
