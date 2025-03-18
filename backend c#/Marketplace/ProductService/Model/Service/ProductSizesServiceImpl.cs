using ProductService.Model.Entity;
using ProductService.Model.Mapper;
using ProductService.Model.Repository;
using ProductService.View.DTO;

namespace ProductService.Model.Service
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

        public async Task<ProductSizesDTO?> GetByProductIdAndSize(int productId, string size)
        {
            return ProductSizesMapper.MapProductSizesToProductSizesDTO(
                           await _productSizesRepository.GetByProductIdAndSize(productId, size)
                   );
        }

        public async Task Update(int productId, List<ProductSizesDTO> newProductsSizes)
        {
            List<ProductSizes> productSizes = newProductsSizes
                .Select(ProductSizesMapper.MapProductSizesDTOToProductSizes)
                .ToList();

            await _productSizesRepository.Update(productId, productSizes);
        }

        public async Task Update(int id, ProductSizesDTO productSizesDTO)
        {
            ProductSizesDTO? currentProductSizes = await GetById(id);

            if (currentProductSizes != null)
            {
                await _productSizesRepository.Update(id,
                    ProductSizesMapper.MapProductSizesDTOToProductSizes(productSizesDTO));
            }
        }
    }
}
