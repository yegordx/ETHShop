using CSharpFunctionalExtensions;

namespace ETHShop.Entities;

public class Seller
{
    public Seller()
    {

    }
    private Seller(Guid id, string storeName, string storeDescription, string contactEmail, string contactPhone)
    {
        SellerID = id;
        StoreName = storeName;
        StoreDescription = storeDescription;
        ContactEmail = contactEmail;
        ContactPhone = contactPhone;
    }
    public Guid SellerID { get; set; } 
    public Guid UserID { get; set; } 
    public string StoreName { get; set; }
    public string StoreDescription { get; set; }
    public string ContactEmail { get; set; }
    public string ContactPhone { get; set; }


    
    public User User { get; set; }
    public ICollection<Product> Products { get; set; }
    public ICollection<Order> Orders { get; set; }

    public static Seller Create(Guid id, string storeName, string storeDescription, string contactEmail, string contactPhone)
    {
        return new Seller(id, storeName, storeDescription, contactEmail, contactPhone);
    }

    public Result SetUser(User user)
    {
        User = user;
        UserID = user.UserId;
        return Result.Success();
    }

    public Result AddProduct(Product product)
    {
        Products.Add(product);
        return Result.Success();
    }
}
