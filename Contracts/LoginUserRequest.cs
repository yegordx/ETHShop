using System.ComponentModel.DataAnnotations;
namespace ETHShop.Contracts;

public record LoginUserRequest(
    [Required] string Email,
    [Required] string Password);
