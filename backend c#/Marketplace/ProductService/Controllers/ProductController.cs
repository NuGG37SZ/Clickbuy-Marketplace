using Microsoft.AspNetCore.Mvc;
using ProductService.DTO;
using ProductService.Service;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("api/v1/products")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productService.GetAll());
        }

        [HttpGet]
        [Route("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            ProductDTO? productDTO = await _productService.GetById(id);

            if (productDTO == null)
                return NotFound("Product Not Found");
            
            return Ok(productDTO);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] ProductDTO productDTO)
        {
            await _productService.Create(productDTO);
            return Created("create", productDTO);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductDTO productDTO)
        {
            ProductDTO? currentProductDTO = await _productService.GetById(id);

            if(currentProductDTO == null)
                return NotFound("Product Not Found");

            await _productService.Update(id, productDTO);
            return Ok(productDTO);
        }

        [HttpDelete]
        [Route("delete/{id}")]
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
