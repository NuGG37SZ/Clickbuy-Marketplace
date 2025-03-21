using ProductService.Model.Entity;

namespace ProductService.Model.Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAll();

        Task<Product?> GetById(int id);

        Task<Product?> GetByProductNameAndUserId(string name, int userId);

        Task<List<Product>> GetByUserId(int userId);

        Task<List<Product>> GetByNameAndUserId(string name, int userId);

        Task<List<Product>> GetByName(string name);

        Task<List<Product>> GetByBrandSubcategoryId(int brandSubcategoryId);

        Task Create(Product product);

        Task Update(int id, Product product);

        Task DeleteById(int id);

        Task DeleteByProductNameAndUserId(string name, int userId);
    }
}
