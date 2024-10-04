using ETHShop.Entities;
namespace ETHShop.Interfaces;

public interface ICategoriesRepository
{
    Task<IEnumerable<Category>> GetAll();
    Task Add(Category category);
    Task Update(Category category);
    Task Delete(Guid id);
    Task<Category> GetById(Guid id);
}
