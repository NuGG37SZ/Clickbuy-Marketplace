﻿using Microsoft.AspNetCore.Mvc;
using RatingService.DTO;
using RatingService.Service;

namespace RatingService.Controllers
{
    [ApiController]
    [Route("api/v1/ratingSeller")]
    public class RatingSellerController : Controller
    {
        private readonly IRatingSellerService _ratingSellerService;

        public RatingSellerController(IRatingSellerService ratingSellerService) => 
            _ratingSellerService = ratingSellerService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _ratingSellerService.GetAll());
        }

        [HttpGet]
        [Route("getByUserId/{userId}")]
        public async Task<IActionResult> GetByProductId(int userId)
        {
            return Ok(await _ratingSellerService.GetByUserId(userId));
        }

        [HttpGet]
        [Route("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            RatingSellerDTO? ratingSellerDTO = await _ratingSellerService.GetById(id);

            if (ratingSellerDTO == null)
                return NotFound("RatingSeller Not Found.");

            return Ok(ratingSellerDTO);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] RatingSellerDTO ratingSellerDTO)
        {
            await _ratingSellerService.Create(ratingSellerDTO);
            return Created("create", ratingSellerDTO);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RatingSellerDTO ratingSellerDTO)
        {
            RatingSellerDTO? currentRatingSellerDTO = await _ratingSellerService.GetById(id);

            if (currentRatingSellerDTO == null)
                return NotFound("RatingSeller Not Found.");

            await _ratingSellerService.Update(id, ratingSellerDTO);
            return Ok(ratingSellerDTO);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            RatingSellerDTO? currentRatingSellerDTO = await _ratingSellerService.GetById(id);

            if (currentRatingSellerDTO == null)
                return NotFound("RatingSeller Not Found.");

            await _ratingSellerService.DeleteById(id);
            return NoContent();
        }
    }
}
