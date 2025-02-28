using ProductService.DTO;
using ProductService.Entity;

namespace ProductService.Mapper
{
    public class CategoryMapper
    {
        public static CategoryDTO MapCategoryToCategoryDTO(Category category)
        {
            CategoryDTO categoryDTO = new CategoryDTO();
            categoryDTO.Name = category.Name;
            return categoryDTO;
        }

        public static Category MapCategoryDTOToCategory(CategoryDTO categoryDTO)
        {
            Category category = new Category();
            category.Name = categoryDTO.Name;
            return category;
        }
    }
}
