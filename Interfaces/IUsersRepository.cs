using ETHShop.Contracts;
using ETHShop.Entities;
namespace ETHShop.Interfaces;

public interface IUsersRepository
{
    Task<IEnumerable<User>> GetAll();
    Task Add(User user);
    Task<User> GetByEmail(string email);
    Task Update(User user);
    Task Delete(string email);
    Task<List<OrderDto>> GetOrders(Guid userID);
}
