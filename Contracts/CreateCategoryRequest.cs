using System.ComponentModel.DataAnnotations;
namespace ETHShop.Contracts;

public record CreateCategoryRequest (string CategoryName, string Description);
