using ProductService.DTO;
using ProductService.Entity;
using ProductService.HttpClient;
using ProductService.Mapper;
using ProductService.Repository;

namespace ProductService.Service
{
    public class ProductServiceImpl : IProductService
    {
        private readonly IProductRepository _productRepository;

        private readonly IUserClient _userClient;

        public ProductServiceImpl(IProductRepository productRepository, IUserClient userClient)
        {
            _productRepository = productRepository;
            _userClient = userClient;
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

        public List<ProductDTO> GetAll()
        {
            return _productRepository.GetAll()
                    .Select(u =>ProductMapper.MapProductToProductDTO(u))
                    .ToList();
        }

        public async Task<ProductDTO> GetById(int id)
        {
            Product product= await _productRepository.GetById(id);
            return ProductMapper.MapProductToProductDTO(product);
        }

        public async Task Update(int id, ProductDTO productDTO)
        {
            Product product = await _productRepository.GetById(id);
            ProductDTO currentProduct = ProductMapper.MapProductToProductDTO(product);

            if(currentProduct != null)
            {
                currentProduct.Price = productDTO.Price;
                currentProduct.Description = productDTO.Description;
                currentProduct.Count = productDTO.Count;
                currentProduct.Name = productDTO.Name;
                currentProduct.UserId = productDTO.UserId;
                _productRepository.Update(id, ProductMapper.MapProductDTOToProduct(currentProduct));
            }
        }
    }
}
