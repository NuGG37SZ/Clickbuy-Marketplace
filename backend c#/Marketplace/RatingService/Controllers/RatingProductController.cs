using Microsoft.AspNetCore.Mvc;
using RatingService.DTO;
using RatingService.Service;

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
            return Ok(await _ratingProductService.GetAll());
        }

        [HttpGet]
        [Route("getByProdcutId/{productId}")]
        public async Task<IActionResult> GetByProductId(int productId)
        {
            return Ok(await _ratingProductService.GetByProductId(productId));
        }

        [HttpGet]
        [Route("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            RatingProductDTO? ratingProductDTO = await _ratingProductService.GetById(id);

            if(ratingProductDTO == null) 
                return NotFound("RatingProduct Not Found.");

            return Ok(ratingProductDTO);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] RatingProductDTO ratingProductDTO)
        {
            await _ratingProductService.Create(ratingProductDTO);
            return Created("create", ratingProductDTO);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RatingProductDTO ratingProductDTO)
        {
            RatingProductDTO? currentRatingProduct = await _ratingProductService.GetById(id);

            if(currentRatingProduct == null)
                return NotFound("RatingProduct Not Found.");

            await _ratingProductService.Update(id, ratingProductDTO);
            return Ok(ratingProductDTO);
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
