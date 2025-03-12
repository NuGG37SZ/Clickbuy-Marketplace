using CartService.DTO;
using CartService.Service;
using Microsoft.AspNetCore.Mvc;

namespace CartService.Controllers
{
    [ApiController]
    [Route("/api/v1/favorites")]
    public class FavoritesController : Controller
    {
        private readonly IFavoritesService _favoriteService;

        public FavoritesController(IFavoritesService favoriteService) => _favoriteService = favoriteService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _favoriteService.GetAll());
        }

        [HttpGet]
        [Route("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            FavoritesDTO? favoritesDTO = await _favoriteService.GetById(id);

            if (favoritesDTO == null) 
                return NotFound("Favorites Not Found.");

            return Ok(favoritesDTO);
        }

        [HttpGet]
        [Route("getByUserId/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            return Ok(await _favoriteService.GetByUserId(userId));
        }

        [HttpGet]
        [Route("getByUserIdAndProductId/{userId}/{productId}")]
        public async Task<IActionResult> GetByUserIdAndProductId(int userId, int productId)
        {
            return Ok(await _favoriteService.GetByUserIdAndProductId(userId, productId));
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] FavoritesDTO favoritesDTO)
        {
            await _favoriteService.Create(favoritesDTO);
            return Created("create", favoritesDTO);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FavoritesDTO favoritesDTO)
        {
            FavoritesDTO? currentFavoritesDTO = await _favoriteService.GetById(id);

            if(currentFavoritesDTO == null)
                return NotFound("Favorites Not Found.");

            await _favoriteService.Update(id, favoritesDTO);
            return Ok(favoritesDTO);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            FavoritesDTO? currentFavoritesDTO = await _favoriteService.GetById(id);

            if(currentFavoritesDTO == null)
                return NotFound("Favorites Not Found.");

            await _favoriteService.DeleteById(id);
            return NoContent();
        }
    }
}
