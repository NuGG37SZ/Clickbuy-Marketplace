using ProductService.Entity;

namespace ProductService.Repository
{
    public interface IProductRepository
    {
        List<Product> GetAll();

        Task<Product> GetById(int id);

        Task Create(Product product);

        Task Update(int id, Product product);

        Task DeleteById(int id);
    }
}
