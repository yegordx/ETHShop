namespace ETHShop.Entities;

public class WishListItem
{
    public WishListItem() { }
    public WishListItem(Guid ID, Product product, Guid wishListID) { 
        WishListItemID = ID;
        WishListID = wishListID;
        ProductID = product.ProductID;
        Product = product;
        DateAdded = DateTime.UtcNow;
    }
    public Guid WishListItemID { get; set; }
    public Guid WishListID { get; set; }
    public Guid ProductID { get; set; }
    public DateTime DateAdded { get; set; }

    
    public WishList WishList { get; set; }
    public Product Product { get; set; }
}
