using ProductService.DTO;
using ProductService.Entity;
using ProductService.Client;
using ProductService.Mapper;
using ProductService.Repository;

namespace ProductService.Service
{
    public class ProductServiceImpl : IProductService
    {
        private readonly IProductRepository _productRepository;

        private readonly IUserClient _userClient;

        private readonly IBrandSubcategoriesService _brandSubcategoriesService;

        public ProductServiceImpl(IProductRepository productRepository, IUserClient userClient,
            IBrandSubcategoriesService brandSubcategoriesService)
        {
            _productRepository = productRepository;
            _userClient = userClient;
            _brandSubcategoriesService = brandSubcategoriesService;
        }

        public async Task Create(ProductDTO productDTO)
        {
            UserDTO userDTO = await _userClient.GetUserById(productDTO.UserId);

            if (userDTO != null)
                await _productRepository.Create(ProductMapper.MapProductDTOToProduct(productDTO));
        }

        public async Task DeleteById(int id)
        {
            await _productRepository.DeleteById(id);
        }

        public async Task DeleteByProductNameAndUserId(string name, int userId)
        {
            await _productRepository.DeleteByProductNameAndUserId(name, userId);
        }

        public async Task<List<ProductDTO>> GetAll()
        {
            List<Product> products = await _productRepository.GetAll();
            return products.Select(ProductMapper.MapProductToProductDTO)
                            .ToList();

        }

        public async Task<ProductDTO?> GetById(int id)
        {
            Product? product = await _productRepository.GetById(id);
            return ProductMapper.MapProductToProductDTO(product);
        }

        public async Task<ProductDTO?> GetByProductNameAndUserId(string name, int userId)
        { 
            return ProductMapper.MapProductToProductDTO(
                await _productRepository.GetByProductNameAndUserId(name, userId)
            );
        }

        public async Task Update(int id, ProductDTO productDTO)
        {
            Product? product = await _productRepository.GetById(id);
            ProductDTO currentProduct = ProductMapper.MapProductToProductDTO(product);

            if(currentProduct != null)
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
