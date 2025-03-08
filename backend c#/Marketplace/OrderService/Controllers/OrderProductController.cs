using Microsoft.AspNetCore.Mvc;
using OrderService.Client;
using OrderService.DTO;
using OrderService.Service;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/v1/orderProduct")]
    public class OrderProductController : Controller
    {
        private readonly IOrderProductService _orderProductService;

        private readonly CartClient _cartClient;

        private readonly IOrderService _orderService;

        public OrderProductController(IOrderProductService orderProductService, 
            CartClient cartClient, IOrderService orderService)
        {
            _orderProductService = orderProductService;
            _cartClient = cartClient;
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _orderProductService.GetAll());
        }

        [HttpGet]
        [Route("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            OrderProductDTO? orderProductDTO = await _orderProductService.GetById(id);

            if (orderProductDTO == null)
                return NotFound("OrderProduct Not Found.");

            return Ok(orderProductDTO);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] OrderProductDTO orderProductDTO)
        {
            CartDTO? cartDTO = await _cartClient.GetCartById(orderProductDTO.CartId);
            OrderDTO orderDTO = await _orderService.GetById(orderProductDTO.OrderId);

            if (cartDTO == null && orderDTO == null)
                return NotFound("Cart and Order Not Found.");
            else if (cartDTO == null)
                return NotFound("Cart Not Found.");
            else if(orderDTO == null)
                return NotFound("Order Not Found.");

            await _orderProductService.Create(orderProductDTO);
            return Created("create", orderProductDTO);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] OrderProductDTO orderProductDTO)
        {
            CartDTO? cartDTO = await _cartClient.GetCartById(orderProductDTO.CartId);
            OrderDTO orderDTO = await _orderService.GetById(orderProductDTO.OrderId);
            OrderProductDTO? currentOrderProduct = await _orderProductService.GetById(id);

            if (cartDTO == null && orderDTO == null && currentOrderProduct == null)
                return NotFound("Cart, Order, OrderProduct Not Found.");
            else if (cartDTO == null)
                return NotFound("Cart Not Found.");
            else if (orderDTO == null)
                return NotFound("Order Not Found.");
            else if (currentOrderProduct == null)
                return NotFound("OrderProduct Not Found.");

            await _orderProductService.Update(id, orderProductDTO);
            return Ok(orderProductDTO);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            OrderProductDTO? currentOrderProduct = await _orderProductService.GetById(id);

            if (currentOrderProduct == null)
                return NotFound("OrderProduct Not Found.");

            await _orderProductService.DeleteById(id);
            return NoContent();
        }
    }
}
