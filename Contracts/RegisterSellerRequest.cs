using System.ComponentModel.DataAnnotations;

namespace ETHShop.Contracts;

public record RegisterSellerRequest (
      [Required] string UserID,
      [Required] string StoreName,
      [Required] string StoreDescription,
      [Required] string ContactEmail,
      [Required] string ContactPhone
);
