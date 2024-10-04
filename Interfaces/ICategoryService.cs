using ETHShop.Contracts;
using ETHShop.Entities;
using ETHShop.Repositories;

namespace ETHShop.Interfaces;

public interface ICategoryService
{
    Task Add(CreateCategoryRequest request);
    Task<IEnumerable<Category>> GetAll();
    Task<Category> GetById(Guid id);
    Task Update(Category category);
    Task Delete(Guid id);
}
