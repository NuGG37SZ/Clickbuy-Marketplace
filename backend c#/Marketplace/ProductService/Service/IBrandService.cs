using ProductService.DTO;

namespace ProductService.Service
{
    public interface IBrandService
    {
        Task<List<BrandDTO>> GetAll();

        Task<BrandDTO?> GetById(int id);

        Task Create(BrandDTO brandDTO);

        Task Update(int id, BrandDTO brandDTO);

        Task DeleteById(int id);
    }
}
