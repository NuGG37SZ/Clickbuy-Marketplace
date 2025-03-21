using System.Net.NetworkInformation;
using OrderService.Migrations;
using OrderService.Model.DTO;
using OrderService.Model.Entity;
using OrderService.View;

namespace OrderService.Model.Mapper
{
    public class OrderMapper
    {
        public static OrderDTO MapOrderToOrderDTO(Order order)
        {
            OrderDTO orderDTO = new OrderDTO();
            orderDTO.Id = order.Id;
            orderDTO.CreateOrder = order.CreateOrder;
            orderDTO.UpdateOrder = order.UpdateOrder;
            orderDTO.UserId = order.UserId;
            orderDTO.Status = order.Status;
            orderDTO.PointId = order.PointId;
            return orderDTO;
        }

        public static Order MapOrderDTOToOrder(OrderDTO orderDTO)
        {
            Order order = new Order();
            order.CreateOrder = orderDTO.CreateOrder;
            order.UpdateOrder = orderDTO.UpdateOrder;
            order.UserId = orderDTO.UserId;
            order.Status = orderDTO.Status;
            order.PointId = orderDTO.PointId;
            return order;
        }

        public static OrderView MapOrderDTOToOrderView(OrderDTO orderDTO)
        {
            OrderView orderView = new OrderView();
            orderView.Id = orderDTO.Id;
            orderView.CreateOrder = orderDTO.CreateOrder;
            orderView.UpdateOrder = orderDTO.UpdateOrder;
            orderView.UserId = orderDTO.UserId;
            orderView.Status = orderDTO.Status;
            orderView.PointId = orderDTO.PointId;
            return orderView;
        }

        public static List<OrderView> MapOrderDTOListToOrderViewList(List<OrderDTO> orderDTOList)
        {
            return orderDTOList.Select(MapOrderDTOToOrderView).ToList();
        }
    }
}
