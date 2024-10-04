using ETHShop.Contracts;
using ETHShop.Entities;
using ETHShop.Repositories;

namespace ETHShop.Interfaces;

public interface IUserService
{
    Task<string> Register(Guid id, string userName, string password, string email, string walletAddress);
    Task<string> Login(LoginUserRequest request);
    Task<bool> AddProductToCart(Guid userID, Guid productID);
    Task<IEnumerable<User>> GetAll();
    Task<User> GetByEmail(string email);
    Task Update(User user);
    Task Delete(string email);
}
