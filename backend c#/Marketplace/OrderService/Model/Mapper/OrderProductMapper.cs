using OrderService.Model.DTO;
using OrderService.Model.Entity;
using OrderService.View;

namespace OrderService.Model.Mapper
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
            orderProductDTO.UserId = orderProduct.UserId;
            return orderProductDTO;
        }

        public static OrderProduct MapOrderProductDTOToOrderProduct(OrderProductDTO orderProductDTO)
        {
            OrderProduct orderProduct = new OrderProduct();
            orderProduct.OrderId = orderProductDTO.OrderId;
            orderProduct.ProductId = orderProductDTO.ProductId;
            orderProduct.ProductSizesId = orderProductDTO.ProductSizesId;
            orderProduct.Count = orderProductDTO.Count;
            orderProduct.UserId = orderProductDTO.UserId;
            return orderProduct;
        }

        public static OrderProductView MapOrderProductDTOToOrderProductView(
            OrderProductDTO orderProductDTO)
        {
            OrderProductView orderProductView = new OrderProductView();
            orderProductView.Id = orderProductDTO.Id;
            orderProductView.OrderId = orderProductDTO.OrderId;
            orderProductView.ProductId = orderProductDTO.ProductId;
            orderProductView.ProductSizesId = orderProductDTO.ProductSizesId;
            orderProductView.Count = orderProductDTO.Count;
            orderProductView.UserId = orderProductDTO.UserId;
            return orderProductView;
        }

        public static List<OrderProductView> MapOrderProductDTOListToOrderProductViewList(
            List<OrderProductDTO> orderProductDTOs)
        {
            return orderProductDTOs.Select(MapOrderProductDTOToOrderProductView).ToList();
        }
    }
}
