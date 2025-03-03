using ProductService.Entity;

namespace ProductService.Repository
{
    public interface IProductSizesRepository
    {
        Task<List<ProductSizes>> GetAll();

        Task<List<ProductSizes>> GetAllByProductId(int productId);

        Task<ProductSizes?> GetById(int id);

        Task Create(ProductSizes productSizes);

        Task DeleteById(int id);
    }
}
