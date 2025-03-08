using OrderService.DTO;
using OrderService.Entity;

namespace OrderService.Mapper
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
            return orderDTO;
        }

        public static Order MapOrderDTOToOrder(OrderDTO orderDTO)
        {
            Order order = new Order();
            order.CreateOrder = orderDTO.CreateOrder;
            order.UpdateOrder = orderDTO.UpdateOrder;
            order.UserId = orderDTO.UserId;
            order.Status = orderDTO.Status;
            return order;
        }
    }
}
