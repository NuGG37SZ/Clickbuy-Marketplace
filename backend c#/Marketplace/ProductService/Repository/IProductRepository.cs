using ProductService.Entity;

namespace ProductService.Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAll();

        Task<Product?> GetById(int id);

        Task<Product?> GetByProductNameAndUserId(string name, int userId);

        Task Create(Product product);

        Task Update(int id, Product product);

        Task DeleteById(int id);

        Task DeleteByProductNameAndUserId(string name, int userId);
    }
}
