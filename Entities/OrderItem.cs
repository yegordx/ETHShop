namespace ETHShop.Entities;

public class OrderItem
{
    public Guid OrderItemID { get; set; }
    public Guid OrderID { get; set; }
    public Guid ProductID { get; set; }
    public int Quantity { get; set; }
    public double TotalPrice { get; set; }
    public double PricePs { get; set; }

    
    public Order Order { get; set; }
    public Product Product { get; set; }
}
