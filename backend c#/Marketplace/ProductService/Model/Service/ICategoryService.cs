using ProductService.Model.DTO;

namespace ProductService.Model.Service
{
    public interface ICategoryService
    {
        Task<List<CategoryDTO>> GetAll();

        Task<CategoryDTO?> GetById(int id);

        Task<CategoryDTO?> GetByName(string name);

        Task Create(CategoryDTO categoryDTO);

        Task Update(int id, CategoryDTO categoryDTO);

        Task DeleteById(int id);
    }
}
