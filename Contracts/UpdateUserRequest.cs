namespace ETHShop.Contracts;

public record UpdateUserRequest (string UserName, string Email, string Password, string WalletAddress);