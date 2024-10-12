using System.ComponentModel.DataAnnotations;

namespace ETHShop.Contracts;

public record AddShippingAddressRequest (
    [Required] string UserId,
    [Required] string Name,
    [Required] string Surname,
    [Required] string Country,
    [Required] string City,
    [Required] string AddressLine,
    string? PostalCode
    );
