using ETHShop.Entities;

namespace ETHShop.Contracts;

public record UpdateSellerRequest(string StoreName, string StoreDescription, string ContactEmail, string ContactPhone);
