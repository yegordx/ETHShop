using ETHShop.Contracts;
using System.ComponentModel.DataAnnotations;

namespace ETHShop.Entities;

public class Review
{
    public Review() { }
    public Review(Guid id, int rating, string comment, User user, Product product) {
        ReviewID = id;
        Rating = rating;
        Comment = comment;
        ReviewDate = DateTime.UtcNow;
        UserID = user.UserId;
        User = user;
        ProductID = product.ProductID;
        Product = product;
    }
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

