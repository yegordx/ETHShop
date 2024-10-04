namespace ETHShop.Entities;

public class ShippingAddress
{
    public Guid AddressID { get; set; }
    public Guid UserID { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string AddressLine { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }

    
    public User User { get; set; }
}
