using ETHShop.Entities;

namespace ETHShop.Interfaces;

public interface ISellersService
{
    Task<string> Register(Guid UserID, string StoreName, string StoreDescription, string ContactEmail, string ContactPhone);

    Task<string> Login(Guid UserID);

    Task<IEnumerable<Seller>> GetAllAsync();

    Task<Seller> GetByIdAsync(Guid SellerID);

    Task UpdateAsync(Seller seller);

    Task DeleteAsync(Guid SellerID);
}
