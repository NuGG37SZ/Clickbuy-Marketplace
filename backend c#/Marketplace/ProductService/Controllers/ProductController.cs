using Microsoft.AspNetCore.Mvc;
using ProductService.Client;
using ProductService.DTO;
using ProductService.Service;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("api/v1/products")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        private readonly IUserClient _userClient;

        private readonly IBrandSubcategoriesService _brandSubcategoriesService;

        public ProductController(IProductService productService, IUserClient userClient,
            IBrandSubcategoriesService brandSubcategoriesService)
        {
            _productService = productService;
            _userClient = userClient;
            _brandSubcategoriesService = brandSubcategoriesService;
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

        [HttpGet]
        [Route("getByNameAndUserId/{name}/{userId}")]
        public async Task<IActionResult> getByNameAndUserId(string name, int userId)
        {
            return Ok(await _productService.GetByProductNameAndUserId(name, userId));
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] ProductDTO productDTO)
        {
            BrandsSubcategoriesDTO? brandSubcategoriesDTO =  
                await _brandSubcategoriesService.GetById(productDTO.BrandsSubcategoriesId);
            UserDTO userDTO = await _userClient.GetUserById(productDTO.UserId);

            if (userDTO == null || brandSubcategoriesDTO == null)
                return NotFound("Error: double check the data");

            await _productService.Create(productDTO);
            return Created("create", productDTO);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductDTO productDTO)
        {
            BrandsSubcategoriesDTO? brandSubcategoriesDTO =
                await _brandSubcategoriesService.GetById(productDTO.BrandsSubcategoriesId);
            UserDTO userDTO = await _userClient.GetUserById(productDTO.UserId);
            ProductDTO? currentProductDTO = await _productService.GetById(id);

            if (userDTO == null || brandSubcategoriesDTO == null)
                return NotFound("Error: double check the data");

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

        [HttpDelete]
        [Route("deleteByNameAndUserId/{name}/{userId}")]
        public async Task<IActionResult> DeleteByNameAndUserId(string name, int userId)
        {
            await _productService.DeleteByProductNameAndUserId(name, userId);
            return NoContent();
        }
    }
}
