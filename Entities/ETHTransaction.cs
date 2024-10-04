namespace ETHShop.Entities;

public class ETHTransaction
{
        public Guid TransactionID { get; set; }
        public Guid OrderID { get; set; }
        public string TransactionHash { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public double AmountETH { get; set; }
        public DateTime TransactionDate { get; set; }

        // Навігаційна властивість
        public Order Order { get; set; }
}
