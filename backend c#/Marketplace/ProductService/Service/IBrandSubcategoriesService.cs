using ProductService.DTO;

namespace ProductService.Service
{
    public interface IBrandSubcategoriesService
    {
        Task<List<BrandsSubcategoriesDTO>> GetAll();

        Task<List<BrandsSubcategoriesDTO>> GetByBrandId(int id);

        Task<List<BrandsSubcategoriesDTO>> GetBySubcategoriesId(int id);

        Task<BrandsSubcategoriesDTO?> GetById(int id);

        Task Create(BrandsSubcategoriesDTO brandSubcategoriesDTO);

        Task DeleteById(int id);


    }
}
