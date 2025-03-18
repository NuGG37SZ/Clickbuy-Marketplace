using ProductService.Model.Entity;
using ProductService.View.DTO;

namespace ProductService.Model.Mapper
{
    public class ProductMapper
    {
        public static ProductDTO MapProductToProductDTO(Product product)
        {
            ProductDTO productDTO = new ProductDTO();
            productDTO.Id = product.Id;
            productDTO.BrandsSubcategoriesId = product.BrandsSubcategoriesId;
            productDTO.UserId = product.UserId;
            productDTO.Price = product.Price;
            productDTO.Description = product.Description;
            productDTO.Name = product.Name;
            productDTO.ImageUrl = product.ImageUrl;
            return productDTO;
        }

        public static Product MapProductDTOToProduct(ProductDTO productDTO)
        {
            Product product = new Product();
            product.BrandsSubcategoriesId = productDTO.BrandsSubcategoriesId;
            product.UserId = productDTO.UserId;
            product.Price = productDTO.Price;
            product.Description = productDTO.Description;
            product.Name = productDTO.Name;
            product.ImageUrl = productDTO.ImageUrl;
            return product;
        }
    }
}
