using Microsoft.AspNetCore.Mvc;
using RatingService.Model.Mapper;
using RatingService.Model.Service;
using RatingService.View.DTO;

namespace RatingService.Controllers
{
    [ApiController]
    [Route("api/v1/ratingProduct")]
    public class RatingProductController : Controller
    {
        private readonly IRatingProductService _ratingProductService;

        public RatingProductController(IRatingProductService ratingProductService) => 
            _ratingProductService = ratingProductService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(RatingProductMapper.MapRatingProductDTOListToRatingProductViewList(
                await _ratingProductService.GetAll()
            ));
        }

        [HttpGet]
        [Route("getByProdcutId/{productId}")]
        public async Task<IActionResult> GetByProductId(int productId)
        {
            return Ok(RatingProductMapper.MapRatingProductDTOListToRatingProductViewList(
                await _ratingProductService.GetByProductId(productId)
            ));
        }

        [HttpGet]
        [Route("getByUserId/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            return Ok(RatingProductMapper.MapRatingProductDTOListToRatingProductViewList(
                await _ratingProductService.GetByUserId(userId)
            ));
        }

        [HttpGet]
        [Route("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            RatingProductDTO? ratingProductDTO = await _ratingProductService.GetById(id);

            if(ratingProductDTO == null) 
                return NotFound("RatingProduct Not Found.");

            return Ok(RatingProductMapper.MapRatingProductDTOToRatingProductView(ratingProductDTO));
        }

        [HttpGet]
        [Route("getByProductIdAndProductSizesIdAndOrderId/{productId}/{productSizesId}/{orderId}")]
        public async Task<IActionResult> GetByProductIdAndProductSizesIdAndOrderId(int productId,
            int productSizesId, int orderId)
        {
            RatingProductDTO? ratingProductDTO = 
                await _ratingProductService.GetByProductIdAndProductSizesIdAndOrderId(
                    productId, productSizesId, orderId);

            if (ratingProductDTO == null)
                return NotFound("RatingProduct Not Found.");

            return Ok(RatingProductMapper.MapRatingProductDTOToRatingProductView(ratingProductDTO));
        }

        [HttpGet]
        [Route("getAvgRatingByProductId/{productId}")]
        public async Task<IActionResult> GetSumRatingByProductId(int productId)
        {
            return Ok(await _ratingProductService.AvgRatingByProductId(productId));
        }

        [HttpGet]
        [Route("countRatingByUserIdAndEmptyComment/{userId}")]
        public async Task<IActionResult> CountRatingByUserIdAndEmptyComment(int userId)
        {
            return Ok(await _ratingProductService.CountRatingByUserIdAndEmptyComment(userId));
        }

        [HttpGet]
        [Route("countEmptyCommentByProductId/{productId}")]
        public async Task<IActionResult> CountEmptyCommentByProductId(int productId)
        {
            return Ok(await _ratingProductService.CountEmptyCommentByProductId(productId));
        }

        [HttpGet]
        [Route("getByUserIdAndProductId/{userId}/{productId}")]
        public async Task<IActionResult> GetByUserIdAndProductId(int userId, int productId)
        {
            return Ok(RatingProductMapper.MapRatingProductDTOListToRatingProductViewList(
                await _ratingProductService.GetByUserIdAndProductId(userId, productId)
            ));
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] RatingProductDTO ratingProductDTO)
        {
            await _ratingProductService.Create(ratingProductDTO);
            return Created("create", 
                RatingProductMapper.MapRatingProductDTOToRatingProductView(ratingProductDTO));
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RatingProductDTO ratingProductDTO)
        {
            RatingProductDTO? currentRatingProduct = await _ratingProductService.GetById(id);

            if(currentRatingProduct == null)
                return NotFound("RatingProduct Not Found.");

            await _ratingProductService.Update(id, ratingProductDTO);
            return Ok(RatingProductMapper.MapRatingProductDTOToRatingProductView(ratingProductDTO));
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            RatingProductDTO? currentRatingProduct = await _ratingProductService.GetById(id);

            if (currentRatingProduct == null)
                return NotFound("RatingProduct Not Found.");

            await _ratingProductService.DeleteById(id);
            return NoContent();
        }
    }
}
