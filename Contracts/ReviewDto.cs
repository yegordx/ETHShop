namespace ETHShop.Contracts;

public record ReviewDto (Guid Id, int Rating, string Comment, Guid UserId);
