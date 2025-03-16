using ProductService.DTO;

namespace ProductService.Service
{
    public interface ISubcategoriesService
    {
        Task<List<SubcategoriesDTO>> GetAll();

        Task<SubcategoriesDTO?> GetById(int id);

        Task<List<SubcategoriesDTO>> GetSubcategoriesByCategoryId(int categoryId);

        Task<SubcategoriesDTO?> GetByName(string name);

        Task Create(SubcategoriesDTO subcategoryDTO);

        Task Update(int id, SubcategoriesDTO subcategoryDTO);

        Task DeleteById(int id);
    }
}
