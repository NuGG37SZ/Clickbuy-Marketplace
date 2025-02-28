using ProductService.DTO;

namespace ProductService.Service
{
    public interface IProductService 
    {
        Task<List<ProductDTO>> GetAll();

        Task<ProductDTO?> GetById(int id);

        Task Create(ProductDTO productDTO);

        Task Update(int id, ProductDTO productDTO);

        Task DeleteById(int id);
    }
}
