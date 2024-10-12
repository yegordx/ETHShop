namespace ETHShop.Contracts;

public record WishListDto ( Guid WishListID, string Name, List<WishListItemDto> WishListItems );
