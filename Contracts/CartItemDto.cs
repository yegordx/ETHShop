namespace ETHShop.Contracts;

public record CartItemDto (Guid CartItemId, string ProductName, double PriceETH, int Quantity, Guid ProductId);