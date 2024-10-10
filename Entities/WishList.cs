using CSharpFunctionalExtensions;

namespace ETHShop.Entities;

public class WishList
{
    public WishList() { }
    public WishList(Guid Id, string WishListName) {
        WishListID = Id;
        CreatedDate = DateTime.UtcNow; 
        Name = WishListName;
    }
    public Guid WishListID { get; set; }
    public Guid UserID { get; set; }
    public DateTime CreatedDate { get; set; }

    public string Name { get; set; }

    public User User { get; set; }
    public HashSet<WishListItem> WishListItems { get; set; }

    public void SetUser(User user)
    {
        User = user;
        UserID = user.UserId;
    }
    public static Result<WishList> Create(Guid id, string Name)
    {
        var wishList = new WishList(id, Name);
        return Result.Success(wishList);
    }
}
