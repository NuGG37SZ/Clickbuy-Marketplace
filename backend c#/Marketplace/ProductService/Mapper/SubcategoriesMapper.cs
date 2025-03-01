using ProductService.DTO;
using ProductService.Entity;

namespace ProductService.Mapper
{
    public class SubcategoriesMapper
    {
        public static SubcategoriesDTO MapSubcategoriesToSubcategoriesDTO(Subcategories subcategories)
        {
            SubcategoriesDTO subcategoriesDTO = new SubcategoriesDTO();
            subcategoriesDTO.Id = subcategories.Id;
            subcategoriesDTO.CategoryId = subcategories.CategoryId;
            subcategoriesDTO.Name = subcategories.Name;
            return subcategoriesDTO;
        }

        public static Subcategories MapSubcategoriesDTOToSubcategories(SubcategoriesDTO subcategoriesDTO)
        {
            Subcategories subcategories = new Subcategories();
            subcategories.CategoryId = subcategoriesDTO.CategoryId;
            subcategories.Name = subcategoriesDTO.Name;
            return subcategories;
        }
    }
}
