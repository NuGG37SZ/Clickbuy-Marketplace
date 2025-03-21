using Microsoft.AspNetCore.Mvc;
using OrderService.Model.DTO;
using OrderService.Model.Mapper;
using OrderService.Model.Service;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/v1/orders")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService) => _orderService = orderService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(OrderMapper.MapOrderDTOListToOrderViewList(await _orderService.GetAll()));
        }

        [HttpGet]
        [Route("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            OrderDTO? orderDTO = await _orderService.GetById(id);

            if(orderDTO == null) 
                return NotFound("Order Not Found.");

            return Ok(OrderMapper.MapOrderDTOToOrderView(orderDTO));
        }

        [HttpGet]
        [Route("getByUserId/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            return Ok(OrderMapper.MapOrderDTOListToOrderViewList(await _orderService.GetByUserId(userId)));
        }

        [HttpGet]
        [Route("getByOrderStatusAndUserId/{status}/{userId}")]
        public async Task<IActionResult> GetByOrderStatusAndUserId(string status, int userId)
        {
            return Ok(OrderMapper.MapOrderDTOListToOrderViewList(
                await _orderService.GetByOrderStatusAndUserId(status, userId)
            ));
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] OrderDTO orderDTO)
        {
            await _orderService.Create(orderDTO);
            return Created("create", OrderMapper.MapOrderDTOToOrderView(orderDTO));
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] OrderDTO orderDTO)
        {
            OrderDTO? currentOrderDTO = await _orderService.GetById(id);

            if (currentOrderDTO == null)
                return NotFound("Order Not Found.");

            await _orderService.Update(id, orderDTO);
            return Ok(OrderMapper.MapOrderDTOToOrderView(orderDTO));
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            OrderDTO? currentOrderDTO = await _orderService.GetById(id);

            if(currentOrderDTO == null)
                return NotFound("Order Not Found.");

            await _orderService.DeleteById(id);
            return NoContent();
        }
    }
}
