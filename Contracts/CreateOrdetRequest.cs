namespace ETHShop.Contracts;

public record CreateOrdetRequest (List<string> ItemsId, string UserId);
