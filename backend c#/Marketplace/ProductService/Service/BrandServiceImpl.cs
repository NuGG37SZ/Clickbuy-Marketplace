using ProductService.DTO;
using ProductService.Entity;
using ProductService.Mapper;
using ProductService.Repository;

namespace ProductService.Service
{
    public class BrandServiceImpl : IBrandService
    {
        private readonly IBrandRepository _brandRepository;

        public BrandServiceImpl(IBrandRepository brandRepository) => _brandRepository = brandRepository;

        public async Task Create(BrandDTO brandDTO)
        {
            await _brandRepository.Create(BrandMapper.MapBrandDTOToBrand(brandDTO));
        }

        public async Task DeleteById(int id)
        {
            await _brandRepository.DeleteById(id);    
        }

        public async Task<List<BrandDTO>> GetAll()
        {
            List<Brand> brands = await _brandRepository.GetAll();
            return brands.Select(BrandMapper.MapBrandToBrandDTO)
                    .ToList();
        }

        public async Task<BrandDTO?> GetById(int id)
        {
            Brand? currentBrand = await _brandRepository.GetById(id);
            
            if(currentBrand != null)
                return BrandMapper.MapBrandToBrandDTO(currentBrand);

            return null;
        }

        public async Task Update(int id, BrandDTO brandDTO)
        {
            BrandDTO? currentBrandDTO = await GetById(id);

            if (currentBrandDTO != null)
            {
                currentBrandDTO.Name = brandDTO.Name;
                await _brandRepository.Update(id, BrandMapper.MapBrandDTOToBrand(currentBrandDTO));
            }
        }
    }
}
