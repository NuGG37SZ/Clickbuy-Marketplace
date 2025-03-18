using ProductService.Model.Entity;
using ProductService.View.DTO;

namespace ProductService.Model.Mapper
{
    public class ProductSizesMapper
    {
        public static ProductSizesDTO MapProductSizesToProductSizesDTO(ProductSizes productSizes)
        {
            ProductSizesDTO productSizesDTO = new ProductSizesDTO();
            productSizesDTO.Id = productSizes.Id;
            productSizesDTO.ProductId = productSizes.ProductId;
            productSizesDTO.Size = productSizes.Size;
            productSizesDTO.Count = productSizes.Count;
            return productSizesDTO;
        }

        public static ProductSizes MapProductSizesDTOToProductSizes(ProductSizesDTO productSizesDTO)
        {
            ProductSizes productSizes = new ProductSizes();
            productSizes.ProductId = productSizesDTO.ProductId;
            productSizes.Size = productSizesDTO.Size;
            productSizes.Count = productSizesDTO.Count;
            return productSizes;
        }
    }
}
