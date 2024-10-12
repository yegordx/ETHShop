using ETHShop.Contracts;
using ETHShop.Entities;
using ETHShop.Repositories;

namespace ETHShop.Interfaces;

public interface IUserService
{
    Task<string> Register(Guid id, string userName, string password, string email, string walletAddress);
    Task<string> Login(string Email, string Password);
    Task<IEnumerable<User>> GetAll();
    Task<User> GetByEmail(string email);
    Task Update(Guid Id, string UserName, string Email, string Password, string WalletAddress);
    Task Delete(Guid Id);
    Task<List<OrderDto>> GetOrders(Guid userID);
}
