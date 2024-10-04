using System.ComponentModel.DataAnnotations;

namespace ETHShop.Entities;

public class Review
{
    public Guid ReviewID { get; set; }
    public Guid ProductID { get; set; }
    public Guid UserID { get; set; }
    [Range(0, 5, ErrorMessage = "Рейтинг має бути від 0 до 5.")]
    public int Rating { get; set; }
    public string Comment { get; set; }
    public DateTime ReviewDate { get; set; }

    
    public Product Product { get; set; }
    public User User { get; set; }
}

