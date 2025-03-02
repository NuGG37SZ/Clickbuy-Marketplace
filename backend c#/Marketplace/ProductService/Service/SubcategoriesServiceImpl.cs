using ProductService.DTO;
using ProductService.Entity;
using ProductService.Mapper;
using ProductService.Repository;

namespace ProductService.Service
{
    public class SubcategoriesServiceImpl : ISubcategoriesService
    {
        private readonly ISubcategoriesRepository _subcategoriesRepository;

        private readonly ICategoryService _categoryService;

        public SubcategoriesServiceImpl(ISubcategoriesRepository subcategoriesRepository,
            ICategoryService categoryService)
        {
            _subcategoriesRepository = subcategoriesRepository;
            _categoryService = categoryService;
        }
            

        public async Task DeleteById(int id)
        {
            SubcategoriesDTO? currentSubcategoriesDTO = await GetById(id);

            if (currentSubcategoriesDTO != null)
                await _subcategoriesRepository.DeleteById(id);
        }

        public async Task<List<SubcategoriesDTO>> GetAll()
        {
            List<Subcategories> subcategories = await _subcategoriesRepository.GetAll();
            return subcategories.Select(SubcategoriesMapper.MapSubcategoriesToSubcategoriesDTO)
                                .ToList();
        }

        public async Task<SubcategoriesDTO?> GetById(int id)
        {
            Subcategories? subcategories = await _subcategoriesRepository.GetById(id);

            if (subcategories != null)
                return SubcategoriesMapper.MapSubcategoriesToSubcategoriesDTO(subcategories);

            return null;

        }

        public async Task Update(int id, SubcategoriesDTO subcategoryDTO)
        {
            SubcategoriesDTO? currentSubcategories = await GetById(id);
            CategoryDTO? categoryDTO = await _categoryService.GetById(subcategoryDTO.CategoryId);

            if(currentSubcategories != null && categoryDTO != null)
            {
                currentSubcategories.CategoryId = subcategoryDTO.CategoryId;
                currentSubcategories.Name = subcategoryDTO.Name;
                await _subcategoriesRepository.Update(id, 
                    SubcategoriesMapper.MapSubcategoriesDTOToSubcategories(currentSubcategories));
            }
        }

        public async Task Create(SubcategoriesDTO subcategoryDTO)
        {
            CategoryDTO? categoryDTO = await _categoryService.GetById(subcategoryDTO.CategoryId);

            if (categoryDTO != null)
            {
                await _subcategoriesRepository.Create(
                    SubcategoriesMapper.MapSubcategoriesDTOToSubcategories(subcategoryDTO)
                );
            }
        }

        public async Task<List<SubcategoriesDTO>> GetSubcategoriesByCategoryId(int categoryId)
        {
            CategoryDTO? categoryDTO = await _categoryService.GetById(categoryId);
            
            if(categoryDTO != null)
            {
                List<Subcategories> subcategories = 
                    await _subcategoriesRepository.GetSubcategoriesByCategoryId(categoryId);
                
                return subcategories
                    .Select(SubcategoriesMapper.MapSubcategoriesToSubcategoriesDTO)
                    .ToList();
            }
            return [];
        }
    }
}
