using CartService.Model.DTO;
using CartService.Model.Mapper;
using CartService.Model.Service;
using Microsoft.AspNetCore.Mvc;

namespace CartService.Controllers
{
    [ApiController]
    [Route("api/v1/carts")]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService) => _cartService = cartService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(CartMapper.MapCartDTOListToCartViewList(await _cartService.GetAll()));
        }

        [HttpGet]
        [Route("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            CartDTO cartDTO = await _cartService.GetById(id);

            if (cartDTO == null)
                return NotFound("Cart Not Found.");

            return Ok(CartMapper.MapCartDTOToCartView(cartDTO));
        }

        [HttpGet]
        [Route("getByUserId/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            return Ok(CartMapper.MapCartDTOListToCartViewList(await _cartService.GetByUserId(userId)));
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CartDTO cartDTO)
        {
            await _cartService.Create(cartDTO);
            return Created("create", CartMapper.MapCartDTOToCartView(cartDTO));
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CartDTO cartDTO)
        {
            CartDTO currentCartDTO = await _cartService.GetById(id);

            if(currentCartDTO == null)
                return NotFound("Cart Not Found.");

            await _cartService.Update(id, cartDTO);
            return Ok(CartMapper.MapCartDTOToCartView(cartDTO));
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

        [HttpDelete]
        [Route("deleteRange")]
        public async Task<IActionResult> DeleteRange([FromBody] List<CartDTO> cartDTOs)
        {
            await _cartService.DeleteRange(cartDTOs);
            return NoContent();
        }
    }
}
