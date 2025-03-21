using ProductService.Model.DTO;

namespace ProductService.Model.Service
{
    public interface IBrandService
    {
        Task<List<BrandsDTO>> GetAll();

        Task<BrandsDTO?> GetById(int id);

        Task<BrandsDTO?> GetByName(string name);

        Task Create(BrandsDTO brandDTO);

        Task Update(int id, BrandsDTO brandDTO);

        Task DeleteById(int id);
    }
}
