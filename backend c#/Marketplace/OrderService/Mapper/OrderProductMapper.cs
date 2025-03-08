using OrderService.DTO;
using OrderService.Entity;

namespace OrderService.Mapper
{
    public class OrderProductMapper
    {
        public static OrderProductDTO MapOrderProductToOrderProductDTO(OrderProduct orderProduct)
        {
            OrderProductDTO orderProductDTO = new OrderProductDTO();
            orderProductDTO.Id = orderProduct.Id;
            orderProductDTO.CartId = orderProduct.CartId;
            orderProductDTO.OrderId = orderProduct.OrderId;
            return orderProductDTO;
        }

        public static OrderProduct MapOrderProductDTOToOrderProduct(OrderProductDTO orderProductDTO)
        {
            OrderProduct orderProduct = new OrderProduct();
            orderProduct.OrderId = orderProductDTO.OrderId;
            orderProduct.CartId = orderProductDTO.CartId;
            return orderProduct;
        }
    }
}
