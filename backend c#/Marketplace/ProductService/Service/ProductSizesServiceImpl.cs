using ProductService.DTO;
using ProductService.Entity;
using ProductService.Mapper;
using ProductService.Repository;

namespace ProductService.Service
{
    public class ProductSizesServiceImpl : IProductSizesService
    {
        private readonly IProductSizesRepository _productSizesRepository;

        public ProductSizesServiceImpl(IProductSizesRepository productSizesRepository) =>
            _productSizesRepository = productSizesRepository;

        public async Task Create(ProductSizesDTO productSizesDTO)
        {
            await _productSizesRepository.Create(ProductSizesMapper.MapProductSizesDTOToProductSizes(productSizesDTO));
        }

        public async Task DeleteById(int id)
        {
            await _productSizesRepository.DeleteById(id);
        }

        public async Task<List<ProductSizesDTO>> GetAll()
        {
            List<ProductSizes> productSizesDTOs = await _productSizesRepository.GetAll();
            return productSizesDTOs
                .Select(ProductSizesMapper.MapProductSizesToProductSizesDTO)
                .ToList();
        }

        public async Task<List<ProductSizesDTO>> GetAllByProductId(int productId)
        {
            List<ProductSizes> productSizesDTOs = await _productSizesRepository.GetAllByProductId(productId);
            return productSizesDTOs
                .Select(ProductSizesMapper.MapProductSizesToProductSizesDTO)
                .ToList();
        }

        public async Task<ProductSizesDTO?> GetById(int id)
        {
            return ProductSizesMapper.MapProductSizesToProductSizesDTO(await _productSizesRepository?.GetById(id));
        }
    }
}
