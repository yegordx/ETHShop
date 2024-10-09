namespace ETHShop.Contracts;

public record OrderItemDto (Guid OrderItemID, Guid ProductID, string ProductName, int Quantity, double TotalPrice);
