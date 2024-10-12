using ETHShop.Contracts;
using ETHShop.Entities;
namespace ETHShop.Interfaces;

public interface IUsersRepository
{
    Task<IEnumerable<User>> GetAll();
    Task Add(User user);
    Task<User> GetById(Guid Id);
    Task<User> GetByEmail(string email);
    Task Update(Guid Id, string UserName, string Email, string HashedPassword, string WalletAddress);
    Task Delete(Guid Id);
    Task<List<OrderDto>> GetOrders(Guid userID);
}
