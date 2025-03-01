using ProductService.DTO;
using ProductService.Entity;
using ProductService.Mapper;
using ProductService.Repository;

namespace ProductService.Service
{
    public class CategoryServiceImpl : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryServiceImpl(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task Create(CategoryDTO categoryDTO)
        {
            await _categoryRepository.Create(CategoryMapper.MapCategoryDTOToCategory(categoryDTO));
        }

        public async Task DeleteById(int id)
        {
            await _categoryRepository.DeleteById(id);
        }

        public async Task<List<CategoryDTO>> GetAll()
        {
            List<Category> categories = await _categoryRepository.GetAll();
            return categories.Select(CategoryMapper.MapCategoryToCategoryDTO)
                                .ToList();
        }

        public async Task<CategoryDTO?> GetById(int id)
        {
            Category? сurrentСategory = await _categoryRepository.GetById(id);

            if (сurrentСategory != null)
                return CategoryMapper.MapCategoryToCategoryDTO(сurrentСategory);

            return null;
        }

        public async Task Update(int id, CategoryDTO categoryDTO)
        {
            CategoryDTO? currentCategoryDTO = await GetById(id);

            if (currentCategoryDTO != null)
            {
                currentCategoryDTO.Name = categoryDTO.Name;
                await _categoryRepository.Update(id, 
                        CategoryMapper.MapCategoryDTOToCategory(currentCategoryDTO));
            }
        }
    }
}
