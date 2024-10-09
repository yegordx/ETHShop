namespace ETHShop.Entities;

public class OrderItem
{
    public OrderItem() { }
    public OrderItem(Guid id, Guid productId, Guid orderId, int quantity, double totalPrice, double pricePs)
    {
        OrderItemID = id;
        ProductID = productId;
        OrderID = orderId;
        Quantity = quantity;
        TotalPrice = totalPrice;
        PricePs = pricePs;
    }
    public Guid OrderItemID { get; set; }
    public Guid OrderID { get; private set; }
    public Guid ProductID { get; set; }
    public int Quantity { get; set; }
    public double TotalPrice { get; set; }
    public double PricePs { get; set; }

    
    public Order Order { get; set; }
    public Product Product { get; set; }

    public void SetOrder(Order order)
    {
        OrderID = order.OrderID;
        Order = order;
    }

}
