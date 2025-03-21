using ProductService.Model.Entity;

namespace ProductService.Model.Repository
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAll();

        Task<Category?> GetById(int id);

        Task<Category?> GetByName(string name);

        Task Create(Category category);

        Task Update(int id, Category category);

        Task DeleteById(int id);
    }
}
