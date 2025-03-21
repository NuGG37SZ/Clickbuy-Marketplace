using ProductService.Model.DTO;
using ProductService.Model.Entity;
using ProductService.View;

namespace ProductService.Model.Mapper
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

        public static SubcategoriesView MapSubcategoriesDTOToSubcategoriesView(SubcategoriesDTO subcategoriesDTO)
        {
            SubcategoriesView subcategoriesView = new SubcategoriesView();
            subcategoriesView.Id = subcategoriesDTO.Id;
            subcategoriesView.CategoryId = subcategoriesDTO.CategoryId;
            subcategoriesView.Name = subcategoriesDTO.Name;
            return subcategoriesView;
        }

        public static List<SubcategoriesView> MapSubcategoriesDTOListToSubcategoriesViewList(
            List<SubcategoriesDTO> subcategoriesDTOList)
        {
            return subcategoriesDTOList.Select(MapSubcategoriesDTOToSubcategoriesView).ToList();
        }
    }
}
