using Microsoft.AspNetCore.Mvc;
using OrderService.Model.DTO;
using OrderService.Model.Mapper;
using OrderService.Model.Service;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/v1/orderProduct")]
    public class OrderProductController : Controller
    {
        private readonly IOrderProductService _orderProductService;

        public OrderProductController(IOrderProductService orderProductService) => 
            _orderProductService = orderProductService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(OrderProductMapper.MapOrderProductDTOListToOrderProductViewList(
                await _orderProductService.GetAll()
            ));
        }

        [HttpGet]
        [Route("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            OrderProductDTO? orderProductDTO = await _orderProductService.GetById(id);

            if (orderProductDTO == null)
                return NotFound("OrderProduct Not Found.");

            return Ok(OrderProductMapper.MapOrderProductDTOToOrderProductView(orderProductDTO));
        }

        [HttpGet]
        [Route("getByOrderId/{orderId}")]
        public async Task<IActionResult> GetByOrderId(int orderId)
        {
            return Ok(OrderProductMapper.MapOrderProductDTOListToOrderProductViewList(
                await _orderProductService.GetByOrderId(orderId)
            ));
        }

        [HttpGet]
        [Route("getByUserId/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            return Ok(OrderProductMapper.MapOrderProductDTOListToOrderProductViewList(
                await _orderProductService.GetByUserId(userId)
            ));
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] OrderProductDTO orderProductDTO)
        {
            await _orderProductService.Create(orderProductDTO);
            return Created("create", OrderProductMapper.MapOrderProductDTOToOrderProductView(
                orderProductDTO));
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] OrderProductDTO orderProductDTO)
        {
            OrderProductDTO? currentOrderProduct = await _orderProductService.GetById(id);

            if (currentOrderProduct == null)
                return NotFound("OrderProduct Not Found.");

            await _orderProductService.Update(id, orderProductDTO);
            return Ok(OrderProductMapper.MapOrderProductDTOToOrderProductView(orderProductDTO));
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
