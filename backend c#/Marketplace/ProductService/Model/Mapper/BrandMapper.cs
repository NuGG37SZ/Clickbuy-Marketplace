using ProductService.Model.DTO;
using ProductService.Model.Entity;
using ProductService.View;

namespace ProductService.Model.Mapper
{
    public class BrandMapper
    {
        public static BrandsDTO MapBrandToBrandDTO(Brands brand)
        {
            BrandsDTO brandDTO = new BrandsDTO();
            brandDTO.Id = brand.Id;
            brandDTO.Name = brand.Name;
            return brandDTO;
        }

        public static Brands MapBrandDTOToBrand(BrandsDTO brandDTO)
        {
            Brands brand = new Brands();
            brand.Name = brandDTO.Name;
            return brand;
        }

        public static BrandsView MapBrandsDTOToBrandsView(BrandsDTO brandDTO)
        {
            BrandsView brandsView = new BrandsView();
            brandsView.Id = brandDTO.Id;
            brandsView.Name = brandDTO.Name;
            return brandsView;
        }

        public static List<BrandsView> MapBrandsDTOListToBrandsViewList(List<BrandsDTO> brandsDTOList)
        {
            return brandsDTOList.Select(BrandMapper.MapBrandsDTOToBrandsView).ToList();
        }
    }
}
