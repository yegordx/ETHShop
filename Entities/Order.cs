namespace ETHShop.Entities;

public class Order
{
    public Guid OrderID { get; set; }
    public Guid UserID { get; set; }
    public Guid SellerID { get; set; }
    public DateTime OrderDate { get; set; }
    public double TotalPriceETH { get; set; }
    public string Status { get; set; } 
    public Seller Seller { get; set; }
    public User User { get; set; }
    public HashSet<OrderItem> OrderItems { get; set; }
    public Payment Payment { get; set; }
    public ETHTransaction EthereumTransaction { get; set; }
}
