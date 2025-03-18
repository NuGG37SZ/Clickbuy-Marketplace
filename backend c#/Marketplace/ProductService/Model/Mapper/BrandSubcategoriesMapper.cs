using ProductService.Model.Entity;
using ProductService.View.DTO;

namespace ProductService.Model.Mapper
{
    public class BrandSubcategoriesMapper
    {
        public static BrandsSubcategoriesDTO MapBrandSubcategoriesToBrandSubcategoriesDTO(
            BrandsSubcategories brandSubcategories)
        {
            BrandsSubcategoriesDTO brandSubcategoriesDTO = new BrandsSubcategoriesDTO();
            brandSubcategoriesDTO.Id = brandSubcategories.Id;
            brandSubcategoriesDTO.BrandsId = brandSubcategories.BrandsId;
            brandSubcategoriesDTO.SubcategoriesId = brandSubcategories.SubcategoriesId;
            return brandSubcategoriesDTO;
        }

        public static BrandsSubcategories MapBrandSubcategoriesDTOToBrandSubcategories(
            BrandsSubcategoriesDTO brandSubcategoriesDTO)
        {
            BrandsSubcategories brandSubcategories = new BrandsSubcategories();
            brandSubcategories.BrandsId = brandSubcategoriesDTO.BrandsId;
            brandSubcategories.SubcategoriesId = brandSubcategoriesDTO.SubcategoriesId;
            return brandSubcategories;
        }
    }
}
