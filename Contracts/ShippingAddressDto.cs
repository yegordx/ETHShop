namespace ETHShop.Contracts;

public record ShippingAddressDto (Guid AddressId, string Name, string Surname, string Country, string City, string AddressLine, string? PostalCode);
