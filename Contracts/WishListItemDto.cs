namespace ETHShop.Contracts;
public record WishListItemDto(Guid WishListItemID, Guid ProductID, string ProductName, DateTime DateAdded);
