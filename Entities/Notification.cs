namespace ETHShop.Entities;

public class Notification
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Value { get; set; }
    public string Status { get; set; }
    public DateTime TimeCreated { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
}
