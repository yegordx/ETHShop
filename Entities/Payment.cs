namespace ETHShop.Entities;

public class Payment
{
    public Guid PaymentID { get; set; }
    public Guid OrderID { get; set; }
    public DateTime PaymentDate { get; set; }
    public double AmountETH { get; set; }
    public string TransactionHash { get; set; }

    // Навігаційна властивість
    public Order Order { get; set; }
}
