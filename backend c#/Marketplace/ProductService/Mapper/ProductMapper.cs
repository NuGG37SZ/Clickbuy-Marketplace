using ProductService.DTO;
using ProductService.Entity;

namespace ProductService.Mapper
{
    public class ProductMapper
    {
        public static ProductDTO MapProductToProductDTO(Product product)
        {
            ProductDTO productDTO = new ProductDTO();
            productDTO.Id = product.Id;
            productDTO.SubcategoriesId = product.SubcategoriesId;
            productDTO.UserId = product.UserId;
            productDTO.Price = product.Price;
            productDTO.Description = product.Description;
            productDTO.Name = product.Name;
            productDTO.Count = product.Count;
            return productDTO;
        }

        public static Product MapProductDTOToProduct(ProductDTO productDTO)
        {
            Product product = new Product();
            product.SubcategoriesId = productDTO.SubcategoriesId;
            product.UserId = productDTO.UserId;
            product.Price = productDTO.Price;
            product.Description = productDTO.Description;
            product.Name = productDTO.Name;
            product.Count = productDTO.Count;
            return product;
        }
    }
}
