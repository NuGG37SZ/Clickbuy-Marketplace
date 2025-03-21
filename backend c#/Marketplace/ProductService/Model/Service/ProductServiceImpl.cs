using ProductService.Model.Repository;
using ProductService.Model.Entity;
using ProductService.Model.Mapper;
using ProductService.Model.DTO;

namespace ProductService.Model.Service
{
    public class ProductServiceImpl : IProductService
    {
        private readonly IProductRepository _productRepository;


        public ProductServiceImpl(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Create(ProductDTO productDTO)
        {
            await _productRepository.Create(ProductMapper.MapProductDTOToProduct(productDTO));
        }

        public async Task DeleteById(int id)
        {
            ProductDTO? productDTO = await GetById(id);

            if (productDTO != null)
                await _productRepository.DeleteById(id);
        }

        public async Task DeleteByProductNameAndUserId(string name, int userId)
        {
            await _productRepository.DeleteByProductNameAndUserId(name, userId);
        }

        public async Task<List<ProductDTO>> GetAll()
        {
            List<Product> products = await _productRepository.GetAll();

            return products
                     .Select(ProductMapper.MapProductToProductDTO)
                     .ToList();

        }

        public async Task<List<ProductDTO>> GetByBrandSubcategoryId(int brandSubcategoryId)
        {
            List<Product> products = await _productRepository.GetByBrandSubcategoryId(brandSubcategoryId);

            return products
                    .Select(ProductMapper.MapProductToProductDTO)
                    .ToList();
        }

        public async Task<ProductDTO?> GetById(int id)
        {
            Product? product = await _productRepository.GetById(id);

            if (product != null)
                return ProductMapper.MapProductToProductDTO(product);

            return null;
        }

        public async Task<List<ProductDTO>> GetByNameAndUserId(string name, int userId)
        {
            List<Product> products = await _productRepository.GetByNameAndUserId(name, userId);

            return products
                    .Select(ProductMapper.MapProductToProductDTO)
                    .ToList();
        }

        public async Task<ProductDTO?> GetByProductNameAndUserId(string name, int userId)
        {
            Product? product = await _productRepository.GetByProductNameAndUserId(name, userId);

            if (product != null)
                return ProductMapper.MapProductToProductDTO(product);

            return null;
        }

        public async Task<List<ProductDTO>> GetByUserId(int userId)
        {
            List<Product> products = await _productRepository.GetByUserId(userId);

            return products
                    .Select(ProductMapper.MapProductToProductDTO)
                    .ToList();
        }

        public async Task Update(int id, ProductDTO productDTO)
        {
            Product? product = await _productRepository.GetById(id);
            ProductDTO currentProduct = ProductMapper.MapProductToProductDTO(product);

            if (currentProduct != null)
            {
                currentProduct.Price = productDTO.Price;
                currentProduct.Description = productDTO.Description;
                currentProduct.Name = productDTO.Name;
                currentProduct.UserId = productDTO.UserId;
                currentProduct.BrandsSubcategoriesId = productDTO.BrandsSubcategoriesId;
                currentProduct.ImageUrl = productDTO.ImageUrl;
                await _productRepository.Update(id, ProductMapper.MapProductDTOToProduct(currentProduct));
            }
        }
    }
}
