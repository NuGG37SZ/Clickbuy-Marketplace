using Microsoft.AspNetCore.Mvc;
using ProductService.Model.DTO;
using ProductService.Model.Mapper;
using ProductService.Model.Service;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("/api/v1/brandsSubcategories")]
    public class BrandSubcategoriesController : Controller
    {
        private readonly IBrandSubcategoriesService _brandSubcategoriesService;

        private readonly IBrandService _brandService;

        private readonly ISubcategoriesService _subcategoriesService;

        public BrandSubcategoriesController(IBrandSubcategoriesService brandSubcategoriesService,
            IBrandService brandService, ISubcategoriesService subcategoriesService)
        {
            _brandSubcategoriesService = brandSubcategoriesService;
            _brandService = brandService;
            _subcategoriesService = subcategoriesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(BrandSubcategoriesMapper.MapBrandsSubcategoriesDTOListToBrandsSubcategoriesViewList(
                await _brandSubcategoriesService.GetAll()
            ));
        }

        [HttpGet]
        [Route("getByBrandId/{id}")]
        public async Task<IActionResult> GetByBrandId(int id)
        {
            BrandsDTO? brandDTO = await _brandService.GetById(id);
            List<BrandsSubcategoriesDTO> brandsSubcategoriesDTO = await _brandSubcategoriesService.GetByBrandId(id);

            if (brandDTO == null) 
                return NotFound("Brand Not Found.");

            return Ok(BrandSubcategoriesMapper.MapBrandsSubcategoriesDTOListToBrandsSubcategoriesViewList(
                brandsSubcategoriesDTO
            ));
        }

        [HttpGet]
        [Route("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            BrandsSubcategoriesDTO? brandsSubcategoriesDTO = await _brandSubcategoriesService.GetById(id);

            if (brandsSubcategoriesDTO == null)
                return NotFound("BrandSubcategories Not Found.");

            return Ok(BrandSubcategoriesMapper.MapBrandSubcategoriesDTOToBrandSubcategoriesView(
                brandsSubcategoriesDTO
            ));
        }

        [HttpGet]
        [Route("getBySubcategoriesId/{id}")]
        public async Task<IActionResult> GetBySubcategoriesdId(int id)
        {
            SubcategoriesDTO? subcategoriesDTO = await _subcategoriesService.GetById(id);
            List<BrandsSubcategoriesDTO> brandsSubcategoriesDTO =
                await _brandSubcategoriesService.GetBySubcategoriesId(id);

            if (subcategoriesDTO == null)
                return NotFound("Subcategories Not Found.");

            return Ok(BrandSubcategoriesMapper.MapBrandsSubcategoriesDTOListToBrandsSubcategoriesViewList(
                brandsSubcategoriesDTO
            ));
        }

        [HttpGet]
        [Route("getByBrandAndSubcategories/{brandId}/{subcategoryId}")]
        public async Task<IActionResult> GetByBrandAndSubcategories(int brandId, int subcategoryId)
        {
            BrandsDTO? brandsDTO = await _brandService.GetById(brandId);
            SubcategoriesDTO? subcategoriesDTO = await _subcategoriesService.GetById(subcategoryId);

            if(brandsDTO != null && subcategoriesDTO != null)
            {
                BrandsSubcategoriesDTO? brandsSubcategoriesDTO =
                    await _brandSubcategoriesService.GetByBrandAndSubcategories(brandId, subcategoryId);

                if (brandsSubcategoriesDTO == null)
                {
                    brandsSubcategoriesDTO = new BrandsSubcategoriesDTO();
                    brandsSubcategoriesDTO.SubcategoriesId = subcategoryId;
                    brandsSubcategoriesDTO.BrandsId = brandId;
                    await _brandSubcategoriesService.Create(brandsSubcategoriesDTO);
                    return Created($"getByBrandAndSubcategories/{brandId}/{subcategoryId}", brandsSubcategoriesDTO);
                } 
                else
                    return Ok(BrandSubcategoriesMapper.MapBrandSubcategoriesDTOToBrandSubcategoriesView(
                        brandsSubcategoriesDTO
                    ));
            }
            return NotFound("Brands or Subcategories do not exist.");
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] BrandsSubcategoriesDTO brandSubcategoriesDTO)
        {
            BrandsDTO? brandDTO = await _brandService.GetById(brandSubcategoriesDTO.BrandsId);
            SubcategoriesDTO? subcategoriesDTO = 
                await _subcategoriesService.GetById(brandSubcategoriesDTO.SubcategoriesId);

            if (brandDTO == null && subcategoriesDTO == null)
                return NotFound("Subcategories and Brand Not Found.");
            else if (brandDTO == null)
                return NotFound("Brand Not Found.");
            else if (subcategoriesDTO == null)
                return NotFound("Subcategories Not Found.");

            await _brandSubcategoriesService.Create(brandSubcategoriesDTO);
            return Ok(BrandSubcategoriesMapper.MapBrandSubcategoriesDTOToBrandSubcategoriesView(
                        brandSubcategoriesDTO
            ));
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            BrandsSubcategoriesDTO? brandSubcategoriesDTO = await _brandSubcategoriesService.GetById(id);

            if (brandSubcategoriesDTO == null)
                return NotFound("BrandSubcategories Not Found.");

            await _brandSubcategoriesService.DeleteById(id);
            return NoContent();
        }
    }
}
