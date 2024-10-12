namespace ETHShop.Entities;

public class ShippingAddress
{
    public ShippingAddress() { }
    public ShippingAddress(Guid addressId, User user, string name, string surname, string country, string city, string addressLine, string? postalcode) 
    {
        AddressID = addressId;
        UserID = user.UserId;
        Name = name;
        Surname = surname;
        Country = country;
        City = city;
        AddressLine = addressLine;
        PostalCode = postalcode;
        User = user;
    }
    public Guid AddressID { get; set; }
    public Guid UserID { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string AddressLine { get; set; }
    public string City { get; set; }
    public string? PostalCode { get; set; }
    public string Country { get; set; }

    
    public User User { get; set; }
}
