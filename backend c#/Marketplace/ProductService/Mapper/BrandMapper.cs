using ProductService.DTO;
using ProductService.Entity;

namespace ProductService.Mapper
{
    public class BrandMapper
    {
        public static BrandDTO MapBrandToBrandDTO(Brand brand)
        {
            BrandDTO brandDTO = new BrandDTO();
            brandDTO.Id = brand.Id;
            brandDTO.Name = brand.Name;
            return brandDTO;
        }

        public static Brand MapBrandDTOToBrand(BrandDTO brandDTO)
        {
            Brand brand = new Brand();
            brand.Name = brandDTO.Name;
            return brand;
        }
    }
}
