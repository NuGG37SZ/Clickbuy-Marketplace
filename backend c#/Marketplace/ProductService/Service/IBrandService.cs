using ProductService.DTO;

namespace ProductService.Service
{
    public interface IBrandService
    {
        Task<List<BrandsDTO>> GetAll();

        Task<BrandsDTO?> GetById(int id);

        Task Create(BrandsDTO brandDTO);

        Task Update(int id, BrandsDTO brandDTO);

        Task DeleteById(int id);
    }
}
