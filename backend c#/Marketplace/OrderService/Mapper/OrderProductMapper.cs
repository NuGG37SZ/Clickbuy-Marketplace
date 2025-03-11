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
            orderProductDTO.OrderId = orderProduct.OrderId;
            orderProductDTO.ProductId = orderProduct.ProductId;
            orderProductDTO.ProductSizesId = orderProduct.ProductSizesId;
            orderProductDTO.Count = orderProduct.Count;
            return orderProductDTO;
        }

        public static OrderProduct MapOrderProductDTOToOrderProduct(OrderProductDTO orderProductDTO)
        {
            OrderProduct orderProduct = new OrderProduct();
            orderProduct.OrderId = orderProductDTO.OrderId;
            orderProduct.ProductId = orderProductDTO.ProductId;
            orderProduct.ProductSizesId = orderProductDTO.ProductSizesId;
            orderProduct.Count = orderProductDTO.Count;
            return orderProduct;
        }
    }
}
