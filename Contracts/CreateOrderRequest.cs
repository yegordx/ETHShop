namespace ETHShop.Contracts;

public record CreateOrderRequest (List<string> ItemsId, string UserId, string AddressId);
