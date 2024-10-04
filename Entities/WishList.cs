namespace ETHShop.Entities;

public class WishList
{
    public Guid WishListID { get; set; }
    public Guid UserID { get; set; }
    public DateTime CreatedDate { get; set; }

    
    public User User { get; set; } = new User();
    public ICollection<WishListItem> WishListItems { get; set; } = new HashSet<WishListItem>();
}
