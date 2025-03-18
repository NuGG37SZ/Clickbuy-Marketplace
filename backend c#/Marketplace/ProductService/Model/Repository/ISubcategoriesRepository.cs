using ProductService.Model.Entity;

namespace ProductService.Model.Repository
{
    public interface ISubcategoriesRepository
    {
        Task<List<Subcategories>> GetAll();

        Task<Subcategories?> GetById(int id);

        Task<List<Subcategories>> GetSubcategoriesByCategoryId(int categoryId);

        Task<Subcategories?> GetByName(string name);

        Task Create(Subcategories subcategory);

        Task Update(int id, Subcategories subcategory);

        Task DeleteById(int id);
    }
}
