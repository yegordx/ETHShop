namespace ETHShop.Entities;

public class Order
{
    public Order() { }
    public Order(Guid id, Guid userId, Guid sellerId)
    {
        OrderID = id;
        UserID = userId;
        SellerID = sellerId;
        OrderDate = DateTime.UtcNow;
        LastTimeEdited = DateTime.UtcNow;
        Status = "Created";
    }
    public Guid OrderID { get; set; }
    public Guid? UserID { get; set; }
    public Guid? SellerID { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime LastTimeEdited { get; set; }
    public double TotalPriceETH { get; set; }
    public string Status { get; set; } 
    public Seller Seller { get; set; }
    public User User { get; set; }
    public HashSet<OrderItem> OrderItems { get; set; }
    public Payment Payment { get; set; }
    public ETHTransaction EthereumTransaction { get; set; }

    public List<OrderItem> AddOrderItems(List<CartItem> cartItems)
    {
        var orderItems = new List<OrderItem>();

        foreach (var cartItem in cartItems)
        {
            var orderItem = new OrderItem(
                Guid.NewGuid(),                          
                cartItem.Product.ProductID,
                OrderID,
                cartItem.Quantity,                       
                cartItem.Quantity * cartItem.Product.PriceETH,    
                cartItem.Product.PriceETH                         
            );

            TotalPriceETH += orderItem.TotalPrice;

            orderItems.Add(orderItem);
        }

        return orderItems;
    }
}
