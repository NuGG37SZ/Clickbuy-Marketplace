using OrderService.Model.Entity;
using OrderService.Model.Mapper;
using OrderService.Model.Repository;
using OrderService.View.DTO;

namespace OrderService.Model.Service
{
    public class OrderServiceImpl : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderServiceImpl(IOrderRepository orderRepository) => _orderRepository = orderRepository;

        public async Task Create(OrderDTO orderDTO)
        {
            await _orderRepository.Create(OrderMapper.MapOrderDTOToOrder(orderDTO));
        }

        public async Task DeleteById(int id)
        {
            await _orderRepository.DeleteById(id);
        }

        public async Task<List<OrderDTO>> GetAll()
        {
            List<Order> orders = await _orderRepository.GetAll();

            return orders
                    .Select(OrderMapper.MapOrderToOrderDTO)
                    .ToList();
        }

        public async Task<OrderDTO?> GetById(int id)
        {
            Order? order = await _orderRepository.GetById(id);

            if (order != null)
                return OrderMapper.MapOrderToOrderDTO(await _orderRepository.GetById(id));

            return null;
        }

        public async Task<List<OrderDTO>> GetByOrderStatusAndUserId(string status, int userId)
        {
            List<Order> orders = await _orderRepository.GetByOrderStatusAndUserId(status, userId);

            return orders
                    .Select(OrderMapper.MapOrderToOrderDTO)
                    .ToList();
        }

        public async Task<List<OrderDTO>> GetByUserId(int userId)
        {
            List<Order> order = await _orderRepository.GetByUserId(userId);

            return order
                    .Select(OrderMapper.MapOrderToOrderDTO)
                    .ToList();
        }

        public async Task Update(int id, OrderDTO orderDTO)
        {
            OrderDTO? currentOrderDTO = await GetById(id);

            if (currentOrderDTO != null)
                await _orderRepository.Update(id, OrderMapper.MapOrderDTOToOrder(orderDTO));
        }
    }
}
