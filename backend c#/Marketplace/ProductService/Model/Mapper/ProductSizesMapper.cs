using ProductService.Model.DTO;
using ProductService.Model.Entity;
using ProductService.View;

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

        public static ProductSizesView MapProductSizesDTOToProductSizesView(ProductSizesDTO productSizesDTO)
        {
            ProductSizesView productSizesView = new ProductSizesView();
            productSizesView.Id = productSizesDTO.Id;
            productSizesView.ProductId = productSizesDTO.ProductId;
            productSizesView.Size = productSizesDTO.Size;
            productSizesView.Count = productSizesDTO.Count;
            return productSizesView;
        }

        public static List<ProductSizesView> MapProductSizesListDTOToProductSizesViewList(
            List<ProductSizesDTO> productSizesDTOs)
        {
            return productSizesDTOs.Select(MapProductSizesDTOToProductSizesView).ToList();
        }
    }
}
