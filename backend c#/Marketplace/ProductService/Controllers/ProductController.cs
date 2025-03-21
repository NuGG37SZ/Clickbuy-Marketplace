using Microsoft.AspNetCore.Mvc;
using ProductService.Model.DTO;
using ProductService.Model.Mapper;
using ProductService.Model.Service;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("api/v1/products")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        private readonly IBrandSubcategoriesService _brandSubcategoriesService;

        public ProductController(IProductService productService,
            IBrandSubcategoriesService brandSubcategoriesService)
        {
            _productService = productService;
            _brandSubcategoriesService = brandSubcategoriesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(ProductMapper.MapProductDTOListToProductViewList(await _productService.GetAll()));
        }

        [HttpGet]
        [Route("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            ProductDTO? productDTO = await _productService.GetById(id);

            if (productDTO == null)
                return NotFound("Product Not Found");
            
            return Ok(ProductMapper.MapProductDTOToProductView(productDTO));
        }

        [HttpGet]
        [Route("getByNameAndUserId/{name}/{userId}")]
        public async Task<IActionResult> GetByNameAndUserId(string name, int userId)
        {
            ProductDTO? productDTO = await _productService.GetByProductNameAndUserId(name, userId);

            if (productDTO == null)
                return NotFound("Product Not Found");

            return Ok(ProductMapper.MapProductDTOToProductView(productDTO));
        }

        [HttpGet]
        [Route("getByUserId/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            return Ok(await _productService.GetByUserId(userId));
        }

        [HttpGet]
        [Route("getProductListByNameAndUserId/{name}/{userId}")]
        public async Task<IActionResult> GetProductListByNameAndUserId(string name, int userId)
        {
            return Ok(ProductMapper.MapProductDTOListToProductViewList(
                await _productService.GetByNameAndUserId(name, userId)
            ));
        }

        [HttpGet]
        [Route("getByBrandSubcategoryId/{brandSubcategoryId}")]
        public async Task<IActionResult> GetByBrandSubcategoryId(int brandSubcategoryId)
        {
            return Ok((ProductMapper.MapProductDTOListToProductViewList(
                await _productService.GetByBrandSubcategoryId(brandSubcategoryId)
            )));
        }

        [HttpGet]
        [Route("getByName/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            return Ok((ProductMapper.MapProductDTOListToProductViewList(
                await _productService.GetByName(name)
            )));
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] ProductDTO productDTO)
        {
            BrandsSubcategoriesDTO? brandSubcategoriesDTO =  
                await _brandSubcategoriesService.GetById(productDTO.BrandsSubcategoriesId);

            if (brandSubcategoriesDTO == null)
                return NotFound("Error: double check the data");

            await _productService.Create(productDTO);
            return Created("create", ProductMapper.MapProductDTOToProductView(productDTO));
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductDTO productDTO)
        {
            BrandsSubcategoriesDTO? brandSubcategoriesDTO =
                await _brandSubcategoriesService.GetById(productDTO.BrandsSubcategoriesId);
            ProductDTO? currentProductDTO = await _productService.GetById(id);

            if (brandSubcategoriesDTO == null)
                return NotFound("Error: double check the data");

            await _productService.Update(id, productDTO);
            return Ok(ProductMapper.MapProductDTOToProductView(productDTO));
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
