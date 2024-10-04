using ETHShop.Entities;

namespace ETHShop.Interfaces;

public interface IProductsService
{
    Task<IEnumerable<Product>> GetAllAsync();

    Task<Product> GetByIdAsync(Guid id);

    Task<bool> EditAsync(Product product);
    Task<bool> AddAsync(Product product, Guid SellerID, string CategoryName);

    Task<bool> DeleteAsync(Guid id);
}

