using ProductService.Model.Entity;
using ProductService.View.DTO;

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
    }
}
