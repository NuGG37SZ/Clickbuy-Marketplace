using ProductService.Model.DTO;

namespace ProductService.Model.Service
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetAll();

        Task<ProductDTO?> GetById(int id);

        Task<ProductDTO?> GetByProductNameAndUserId(string name, int userId);

        Task<List<ProductDTO>> GetByUserId(int userId);

        Task<List<ProductDTO>> GetByNameAndUserId(string name, int userId);

        Task<List<ProductDTO>> GetByBrandSubcategoryId(int brandSubcategoryId);

        Task Create(ProductDTO productDTO);

        Task Update(int id, ProductDTO productDTO);

        Task DeleteById(int id);

        Task DeleteByProductNameAndUserId(string name, int userId);
    }
}
