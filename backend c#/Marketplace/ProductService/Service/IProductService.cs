using ProductService.Entity;

namespace ProductService.Service
{
    public interface IProductService 
    {
        List<Product> GetAll();

        Task<Product> GetById(int id);

        Task Create(Product product);

        Task Update(int id, Product product);

        Task DeleteById(int id);
    }
}
