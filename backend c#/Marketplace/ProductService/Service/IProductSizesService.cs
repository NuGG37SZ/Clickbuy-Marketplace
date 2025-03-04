using ProductService.DTO;

namespace ProductService.Service
{
    public interface IProductSizesService
    {
        Task<List<ProductSizesDTO>> GetAll();

        Task<List<ProductSizesDTO>> GetAllByProductId(int productId);

        Task<ProductSizesDTO?> GetById(int id);

        Task Create(ProductSizesDTO productSizesDTO);

        Task DeleteById(int id);

        Task Update(int productId, List<ProductSizesDTO> newProductsSizes);
    }
}
