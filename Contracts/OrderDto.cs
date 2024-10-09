namespace ETHShop.Contracts;

public record OrderDto(Guid OrderID, Guid? SellerID, double TotalPrice, List<OrderItemDto> OrderItems, DateTime OrderDate );

