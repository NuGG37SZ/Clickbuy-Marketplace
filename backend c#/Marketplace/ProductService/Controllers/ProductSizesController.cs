using Microsoft.AspNetCore.Mvc;
using ProductService.Model.DTO;
using ProductService.Model.Mapper;
using ProductService.Model.Service;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("api/v1/productSizes")]
    public class ProductSizesController : Controller
    {
        private readonly IProductSizesService _productSizesService;

        private readonly IProductService _productService;

        public ProductSizesController(IProductSizesService productSizesService, 
            IProductService productService)
        {
            _productSizesService = productSizesService;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(ProductSizesMapper.MapProductSizesListDTOToProductSizesViewList(
                await _productSizesService.GetAll()
            ));
        }

        [HttpGet]
        [Route("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            ProductSizesDTO? productSizesDTO = await _productSizesService.GetById(id);

            if (productSizesDTO == null)
                return NotFound("ProductSizes Not Found.");

            return Ok(ProductSizesMapper.MapProductSizesDTOToProductSizesView(productSizesDTO));
        }

        [HttpGet]
        [Route("getAllByProductId/{productId}")]
        public async Task<IActionResult> GetAllByProductId(int productId)
        {
            return Ok(ProductSizesMapper.MapProductSizesListDTOToProductSizesViewList(
                await _productSizesService.GetAllByProductId(productId)
            ));
        }

        [HttpGet]
        [Route("getByProductIdAndSize/{productId}/{size}")]
        public async Task<IActionResult> GetByProductIdAndSize(int productId, string size)
        {
            ProductDTO? productDTO = await _productService.GetById(productId);

            if (productDTO == null)
                return NotFound("Product Not Found.");

            ProductSizesDTO? productSizesDTO = await _productSizesService.GetByProductIdAndSize(productId, size);
            return Ok(ProductSizesMapper.MapProductSizesDTOToProductSizesView(productSizesDTO));
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] ProductSizesDTO productSizesDTO)
        {
            await _productSizesService.Create(productSizesDTO);
            return Created("create", ProductSizesMapper.MapProductSizesDTOToProductSizesView(productSizesDTO));
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

        [HttpPut]
        [Route("update/{productId}")]
        public async Task<IActionResult> Update(int productId, [FromBody] List<ProductSizesDTO> productSizes)
        {
            List<ProductSizesDTO> productSizesDTOs = await _productSizesService.GetAllByProductId(productId);

            if (productSizesDTOs == null)
                return NotFound("Could not find products by this id");

            await _productSizesService.Update(productId, productSizes);
            return Ok(ProductSizesMapper.MapProductSizesListDTOToProductSizesViewList(productSizes));
        }

        [HttpPut]
        [Route("updateById/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductSizesDTO productSizesDTO)
        {
            ProductSizesDTO? currentProductSizesDTO = await _productSizesService.GetById(id);

            if( currentProductSizesDTO == null)
                return NotFound("ProductSizes Not Found.");

            await _productSizesService.Update(id, productSizesDTO);
            return Ok(ProductSizesMapper.MapProductSizesDTOToProductSizesView(currentProductSizesDTO));
        }
    }
}

