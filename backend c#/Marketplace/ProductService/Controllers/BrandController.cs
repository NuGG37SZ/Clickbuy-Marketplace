using Microsoft.AspNetCore.Mvc;
using ProductService.Model.Service;
using ProductService.View.DTO;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("/api/v1/brands")]
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService) => _brandService = brandService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _brandService.GetAll());
        }

        [HttpGet]
        [Route("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            BrandsDTO? brandDTO = await _brandService.GetById(id);

            if (brandDTO == null)
                return NotFound("Brand Not Found.");

            return Ok(brandDTO);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] BrandsDTO brandDTO)
        {
            await _brandService.Create(brandDTO);
            return Created("create", brandDTO);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BrandsDTO brandDTO)
        {
            BrandsDTO? currentBrand = await _brandService.GetById(id);

            if (currentBrand == null)
                return NotFound("Brand Not Found.");

            await _brandService.Update(id, brandDTO);
            return Ok(brandDTO);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            BrandsDTO? brandDTO = await _brandService.GetById(id);

            if (brandDTO == null)
                return NotFound("Brand Not Found.");

            await _brandService.DeleteById(id);
            return NoContent();
        }
    }
}
