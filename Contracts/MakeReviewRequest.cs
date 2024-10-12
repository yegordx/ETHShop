namespace ETHShop.Contracts;

public record MakeReviewRequest(int Rating, string Comment, string UserId, string ProductId);