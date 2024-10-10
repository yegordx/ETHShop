using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Runtime.CompilerServices;

namespace ETHShop.Entities;

public class User
{
    public User() { }
    public User(Guid id, string name, string email, string passwordhash, string walletaddress)
    {
        UserId = id;
        UserName = name;
        Email = email;
        PasswordHash = passwordhash;
        if (!string.IsNullOrWhiteSpace(walletaddress))
        {
            WalletAddress = walletaddress;
        }
        else
        {
            WalletAddress = "";
        }
        RegisteredAt = DateTime.UtcNow;
    }
    public Guid UserId { get; private set; }
    public string UserName { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public string WalletAddress { get; private set; }
    public DateTime RegisteredAt { get; private set; }

    public Guid SellerId { get; private set; }
    public Guid ShoppingCartID { get; private set; }
    public ICollection<Order> Orders { get; set; }
    public ICollection<Review> Reviews { get; set; }
    public ICollection<ShippingAddress> ShippingAddresses { get; set; }
    public Seller Seller { get; set; }
    public ShoppingCart ShoppingCart { get; set; }
    public List<WishList> WishLists { get; set; } = new List<WishList>();
    public ICollection<Notification> Notifications { get; set; }

    public static User Create(Guid id, string userName, string PasswordHash, string email, string walletAddress)
    {
        //if (EmailChecker.IsValidEmail(email))
        //{
        //    throw new ArgumentException("Invalid email format");
        //}

        return new User(id, userName, email, PasswordHash, walletAddress);
    }

    public Result SetShoppingCart(ShoppingCart shoppingCart)
    {
        ShoppingCartID = shoppingCart.CartID;
        ShoppingCart = shoppingCart;
        return Result.Success(shoppingCart);
    }

    public Result AddWishList(WishList wishList)
    {
        WishLists.Add(wishList);
        return Result.Success(wishList);
    }

    public Result SetSeller(Seller seller)
    {
        SellerId = seller.SellerID;
        Seller = seller;
        return Result.Success(seller);
    }
}
