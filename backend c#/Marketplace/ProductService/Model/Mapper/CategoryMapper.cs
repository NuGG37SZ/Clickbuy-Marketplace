using ProductService.Model.Entity;
using ProductService.View.DTO;

namespace ProductService.Model.Mapper
{
    public class CategoryMapper
    {
        public static CategoryDTO MapCategoryToCategoryDTO(Category category)
        {
            CategoryDTO categoryDTO = new CategoryDTO();
            categoryDTO.Id = category.Id;
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
