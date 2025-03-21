using ProductService.Model.DTO;
using ProductService.Model.Entity;
using ProductService.View;

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

        public static CategoryView MapCategoryDTOToCategoryView(CategoryDTO categoryDTO)
        {
            CategoryView categoryView = new CategoryView();
            categoryView.Id = categoryDTO.Id;
            categoryView.Name = categoryDTO.Name;
            return categoryView;
        }

        public static List<CategoryView> MapCategoryDTOListToCategoryViewList(List<CategoryDTO> categoryDTOList)
        {
            return categoryDTOList.Select(MapCategoryDTOToCategoryView).ToList();
        }
    }
}
