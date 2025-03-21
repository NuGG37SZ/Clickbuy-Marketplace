using ProductService.Model.DTO;
using ProductService.Model.Entity;
using ProductService.View;

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

        public static BrandsSubcategoriesView MapBrandSubcategoriesDTOToBrandSubcategoriesView(
            BrandsSubcategoriesDTO brandsSubcategoriesDTO)
        {
            BrandsSubcategoriesView brandsSubcategoriesView = new BrandsSubcategoriesView();
            brandsSubcategoriesView.Id = brandsSubcategoriesDTO.Id;
            brandsSubcategoriesView.BrandsId = brandsSubcategoriesDTO.BrandsId;
            brandsSubcategoriesView.SubcategoriesId = brandsSubcategoriesDTO.SubcategoriesId;
            return brandsSubcategoriesView;
        }

        public static List<BrandsSubcategoriesView> MapBrandsSubcategoriesDTOListToBrandsSubcategoriesViewList(
            List<BrandsSubcategoriesDTO> brandsSubcategoriesDTOs)
        {
            return brandsSubcategoriesDTOs.Select(MapBrandSubcategoriesDTOToBrandSubcategoriesView).ToList();
        }
    }
}
