﻿using Microsoft.AspNetCore.Mvc;
using ProductService.DTO;
using ProductService.Service;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("api/v1/productSizes")]
    public class ProductSizesController : Controller
    {
        private readonly IProductSizesService _productSizesService;

        private readonly IProductService _productService;

        public ProductSizesController(IProductSizesService productSizesService, IProductService productService)
        {
            _productSizesService = productSizesService;
            _productService = productService;
        }

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

        [HttpGet]
        [Route("getByProductIdAndSize/{productId}/{size}")]
        public async Task<IActionResult> GetByProductIdAndSize(int productId, string size)
        {
            ProductDTO? productDTO = await _productService.GetById(productId);

            if (productDTO == null)
                return NotFound("Product Not Found.");

            ProductSizesDTO? productSizesDTO = await _productSizesService.GetByProductIdAndSize(productId, size);
            return Ok(productSizesDTO);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] ProductSizesDTO productSizesDTO)
        {
            await _productSizesService.Create(productSizesDTO);
            return Created("create", productSizesDTO);
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
            return Ok(productSizes);
        }
    }
}
