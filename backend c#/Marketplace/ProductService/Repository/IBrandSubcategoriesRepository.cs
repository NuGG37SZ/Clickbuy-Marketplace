using ProductService.Entity;

namespace ProductService.Repository
{
    public interface IBrandSubcategoriesRepository
    {
        Task<List<BrandsSubcategories>> GetAll();

        Task<List<BrandsSubcategories>> GetByBrandId(int id);

        Task<List<BrandsSubcategories>> GetBySubcategoriesId(int id);

        Task<BrandsSubcategories?> GetById(int id);

        Task Create(BrandsSubcategories brandSubcategories);

        Task DeleteById(int id);
    }
}
