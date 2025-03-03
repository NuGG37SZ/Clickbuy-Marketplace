using Microsoft.AspNetCore.Mvc;
using ProductService.DTO;
using ProductService.Service;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("api/v1/productSizes")]
    public class ProductSizesController : Controller
    {
        private readonly IProductSizesService _productSizesService;

        public ProductSizesController(IProductSizesService productSizesService) => _productSizesService = productSizesService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productSizesService.GetAll());
        }

        [HttpGet]
        [Route("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            ProductSizesDTO? productSizesDTO = await _productSizesService.GetById(id);

            if (productSizesDTO == null)
                return NotFound("ProductSizes Not Found.");

            return Ok(productSizesDTO);
        }

        [HttpGet]
        [Route("getAllByProductId/{productId}")]
        public async Task<IActionResult> GetAllByProductId(int productId)
        {
            List<ProductSizesDTO>? productSizesDTOs = await _productSizesService.GetAllByProductId(productId);

            if(productSizesDTOs == null)
                return NotFound("ProductSizes Not Found.");

            return Ok(productSizesDTOs);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] ProductSizesDTO productSizesDTO)
        {
            return Created("create", _productSizesService.Create(productSizesDTO));
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            ProductSizesDTO? productSizesDTO = await _productSizesService.GetById(id);

            if( productSizesDTO == null)
                return NotFound("ProductSizes Not Found.");

            await _productSizesService.DeleteById(id);
            return NoContent();
        }
    }
}
