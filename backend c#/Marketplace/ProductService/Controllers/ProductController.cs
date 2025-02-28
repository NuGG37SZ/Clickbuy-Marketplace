using Microsoft.AspNetCore.Mvc;
using ProductService.DTO;
using ProductService.Service;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("products")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productService.GetAll());
        }

        [HttpGet]
        [Route("products/getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            ProductDTO? productDTO = await _productService.GetById(id);

            if (productDTO == null)
                return NotFound("Product Not Found");
            
            return Ok(productDTO);
        }

        [HttpPost]
        [Route("products/create")]
        public async Task<IActionResult> Create([FromBody] ProductDTO productDTO)
        {
            await _productService.Create(productDTO);
            return Ok(productDTO);
        }

        [HttpPut]
        [Route("products/update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductDTO productDTO)
        {
            ProductDTO? currentProductDTO = await _productService.GetById(id);

            if(currentProductDTO == null)
                return NotFound("Product Not Found");

            await _productService.Update(id, productDTO);
            return Ok(productDTO);
        }

        [HttpDelete]
        [Route("products/delete/{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            ProductDTO? currentProductDTO = await _productService.GetById(id);

            if (currentProductDTO == null)
                return NotFound("Product Not Found");

            await _productService.DeleteById(id);
            return NoContent();
        }
    }
}
