namespace ETHShop.Entities;

public class WishListItem
{
    public Guid WishListItemID { get; set; }
    public Guid WishListID { get; set; }
    public Guid ProductID { get; set; }
    public DateTime DateAdded { get; set; }

    
    public WishList WishList { get; set; } = new WishList();
    public Product Product { get; set; } = new Product();
}
