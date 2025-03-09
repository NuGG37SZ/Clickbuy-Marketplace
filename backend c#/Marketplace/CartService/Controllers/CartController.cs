using CartService.DTO;
using CartService.Service;
using Microsoft.AspNetCore.Mvc;

namespace CartService.Controllers
{
    [ApiController]
    [Route("api/v1/carts")]
    public class CartController(ICartService cartService) : Controller
    {
        private readonly ICartService _cartService = cartService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _cartService.GetAll());
        }

        [HttpGet]
        [Route("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            CartDTO cartDTO = await _cartService.GetById(id);

            if (cartDTO == null)
                return NotFound("Cart Not Found.");

            return Ok(cartDTO);
        }

        [HttpGet]
        [Route("getByUserId/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            List<CartDTO> cartDTOs = await _cartService.GetByUserId(userId);

            if (cartDTOs == null)
                return NotFound("Cart is empty :ñ");

            return Ok(cartDTOs);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CartDTO cartDTO)
        {
            await _cartService.Create(cartDTO);
            return Created("create", cartDTO);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CartDTO cartDTO)
        {
            CartDTO currentCartDTO = await _cartService.GetById(id);

            if(currentCartDTO == null)
                return NotFound("Cart Not Found.");

            await _cartService.Update(id, cartDTO);
            return Ok(cartDTO);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            CartDTO cartDTO = await _cartService.GetById(id);

            if(cartDTO == null)
                return NotFound("Cart Not Found.");

            await _cartService.DeleteById(id);
            return NoContent();
        }
        
    }
}
