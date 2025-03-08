using OrderService.DTO;
using OrderService.Entity;
using OrderService.Mapper;
using OrderService.Repository;

namespace OrderService.Service
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

        public async Task<OrderDTO> GetById(int id)
        {
            Order? order = await _orderRepository.GetById(id);

            if (order != null)
                return OrderMapper.MapOrderToOrderDTO(await _orderRepository.GetById(id));

            return null;
        }

        public async Task Update(int id, OrderDTO orderDTO)
        {
            await _orderRepository.Update(id, OrderMapper.MapOrderDTOToOrder(orderDTO)); 
        }
    }
}
