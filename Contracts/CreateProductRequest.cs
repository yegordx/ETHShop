namespace ETHShop.Contracts;

public record CreateProductRequest (string SellerID, string CategoryName, string ProductName, string Description, double PriceETH);