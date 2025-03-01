using ProductService.DTO;
using ProductService.Entity;
using ProductService.Mapper;
using ProductService.Repository;

namespace ProductService.Service
{
    public class BrandSubcategoriesServiceImpl : IBrandSubcategoriesService
    {
        private readonly IBrandSubcategoriesRepository _brandSubcategoriesRepository;

        private readonly IBrandService _brandService;

        private readonly ISubcategoriesService _subcategoriesService;

        public BrandSubcategoriesServiceImpl(IBrandSubcategoriesRepository brandSubcategoriesRepository, 
            IBrandService brandService, ISubcategoriesService subcategoriesService)
        {
            _brandSubcategoriesRepository = brandSubcategoriesRepository;
            _brandService = brandService;
            _subcategoriesService = subcategoriesService;
        }

        public async Task Create(BrandsSubcategoriesDTO brandSubcategoriesDTO)
        {
            BrandsDTO? currentBrandDTO = await _brandService.GetById(brandSubcategoriesDTO.BrandsId);
            SubcategoriesDTO? currentSubcategoriesDTO =
                await _subcategoriesService.GetById(brandSubcategoriesDTO.SubcategoriesId);

            if (currentBrandDTO != null && currentSubcategoriesDTO != null)
            {
                await _brandSubcategoriesRepository.Create(
                    BrandSubcategoriesMapper.MapBrandSubcategoriesDTOToBrandSubcategories(
                        brandSubcategoriesDTO
                    ));
            }
        }

        public async Task DeleteById(int id)
        {
            BrandsSubcategoriesDTO? brandsSubcategories = await GetById(id);

            if (brandsSubcategories != null)
                await _brandSubcategoriesRepository.DeleteById(id);
            
        }

        public async Task<List<BrandsSubcategoriesDTO>> GetAll()
        {
            List<BrandsSubcategories> brandSubcategories = await _brandSubcategoriesRepository.GetAll();
            return brandSubcategories
                .Select(BrandSubcategoriesMapper.MapBrandSubcategoriesToBrandSubcategoriesDTO)
                .ToList();
        }

        public async Task<List<BrandsSubcategoriesDTO>> GetByBrandId(int id)
        {
            BrandsDTO? currentBrandDTO = await _brandService.GetById(id);

            if (currentBrandDTO != null)
            {
                List<BrandsSubcategories> brands = await _brandSubcategoriesRepository.GetByBrandId(id);
                return brands
                    .Select(BrandSubcategoriesMapper.MapBrandSubcategoriesToBrandSubcategoriesDTO)
                    .ToList();
            }
            return [];
        }

        public async Task<BrandsSubcategoriesDTO?> GetById(int id)
        {
            BrandsSubcategories? brandsSubcategories = await _brandSubcategoriesRepository.GetById(id);

            if (brandsSubcategories != null)
                return BrandSubcategoriesMapper.MapBrandSubcategoriesToBrandSubcategoriesDTO(brandsSubcategories);
            
            return null;
        }

        public async Task<List<BrandsSubcategoriesDTO>> GetBySubcategoriesId(int id)
        {
            SubcategoriesDTO? currentSubcategoriesDTO = await _subcategoriesService.GetById(id);

            if(currentSubcategoriesDTO != null)
            {
                List<BrandsSubcategories> subcategories =
                    await _brandSubcategoriesRepository.GetBySubcategoriesId(id);
                return subcategories
                    .Select(BrandSubcategoriesMapper.MapBrandSubcategoriesToBrandSubcategoriesDTO)
                    .ToList();
            }
            return [];
        }
    }
}
