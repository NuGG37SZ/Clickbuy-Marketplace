using ProductService.Entity;

namespace ProductService.Repository
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAll();

        Task<Category?> GetById(int id);

        Task Create(Category category);

        Task Update(int id, Category category);

        Task DeleteById(int id);
    }
}
