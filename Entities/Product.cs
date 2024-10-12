using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;

namespace ETHShop.Entities;

public class Product
{
    public Product() { }
    public Product(Guid Id, string name, string descriprion, double priceETH) 
    { 
        ProductID = Id;
        ProductName = name;
        Description = descriprion;
        PriceETH = priceETH;
        DateAdded = DateTime.UtcNow;
    }

    public Guid ProductID { get; set; }
    public Guid SellerID { get; set; }
    public string ProductName { get; set; }
    public string Description { get; set; }
    public double PriceETH { get; set; }
    public Guid CategoryID { get; set; }
    public DateTime DateAdded { get; set; }

    
    public Seller Seller { get; set; }
    public Category Category { get; set; }
    public ICollection<Review> Reviews { get; set; } = new List<Review>();

    public Result SetCategory(Category category)
    {
        Category = category;
        CategoryID = category.CategoryID;
        return Result.Success();
    }

    public Result SetSeller(Seller seller)
    {
        Seller = seller;
        SellerID = seller.SellerID;
        return Result.Success();
    }

    public Result AddReview(Review review)
    {
        Reviews.Add(review);
        return Result.Success(review);
    }
}