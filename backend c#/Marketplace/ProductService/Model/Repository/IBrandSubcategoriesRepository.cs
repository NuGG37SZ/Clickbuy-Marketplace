using ProductService.Model.Entity;

namespace ProductService.Model.Repository
{
    public interface IBrandSubcategoriesRepository
    {
        Task<List<BrandsSubcategories>> GetAll();

        Task<List<BrandsSubcategories>> GetByBrandId(int id);

        Task<List<BrandsSubcategories>> GetBySubcategoriesId(int id);

        Task<BrandsSubcategories> GetByBrandAndSubcategories(int brandId, int subcategoryId);

        Task<BrandsSubcategories?> GetById(int id);

        Task Create(BrandsSubcategories brandSubcategories);

        Task DeleteById(int id);
    }
}
