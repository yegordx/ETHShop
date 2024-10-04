using System.ComponentModel.DataAnnotations;
namespace ETHShop.Contracts;

public record RegisterUserRequest (
    [Required] string UserName,
    [Required] string Email,
    [Required] string Password, 
    [Required] string WalletAddress);
