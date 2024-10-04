using CSharpFunctionalExtensions;

namespace ETHShop.Entities;

public class Category
{
    public Category() { }
    private Category(Guid id, string categoryName, string description)
    {
        CategoryID = id;
        CategoryName = categoryName;
        Description = description; 
    }
    public Guid CategoryID { get; set; }
    public string CategoryName { get; set; }
    public string Description { get; set; }

    // Навігаційна властивість
    public ICollection<Product> Products { get; set; } = new List<Product>();

    public static Category Create(Guid id, string categoryName, string description)
    {
        return new Category(id, categoryName, description);
    }

    public Result AddProduct(Product product)
    {
        Products.Add(product);
        return Result.Success(product);
    }
}
