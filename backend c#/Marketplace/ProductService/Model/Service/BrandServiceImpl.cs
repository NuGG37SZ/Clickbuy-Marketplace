using ProductService.Model.Entity;
using ProductService.Model.Mapper;
using ProductService.Model.Repository;
using ProductService.View.DTO;

namespace ProductService.Model.Service
{
    public class BrandServiceImpl : IBrandService
    {
        private readonly IBrandRepository _brandRepository;

        public BrandServiceImpl(IBrandRepository brandRepository) => _brandRepository = brandRepository;

        public async Task Create(BrandsDTO brandDTO)
        {
            await _brandRepository.Create(BrandMapper.MapBrandDTOToBrand(brandDTO));
        }

        public async Task DeleteById(int id)
        {
            await _brandRepository.DeleteById(id);
        }

        public async Task<List<BrandsDTO>> GetAll()
        {
            List<Brands> brands = await _brandRepository.GetAll();
            return brands.Select(BrandMapper.MapBrandToBrandDTO)
                    .ToList();
        }

        public async Task<BrandsDTO?> GetById(int id)
        {
            Brands? currentBrand = await _brandRepository.GetById(id);

            if (currentBrand != null)
                return BrandMapper.MapBrandToBrandDTO(currentBrand);

            return null;
        }

        public async Task Update(int id, BrandsDTO brandDTO)
        {
            BrandsDTO? currentBrandDTO = await GetById(id);

            if (currentBrandDTO != null)
            {
                currentBrandDTO.Name = brandDTO.Name;
                await _brandRepository.Update(id, BrandMapper.MapBrandDTOToBrand(currentBrandDTO));
            }
        }
    }
}
