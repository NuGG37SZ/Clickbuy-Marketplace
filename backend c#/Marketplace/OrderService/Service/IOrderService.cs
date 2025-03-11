using OrderService.DTO;

namespace OrderService.Service
{
    public interface IOrderService
    {
        Task<List<OrderDTO>> GetAll();

        Task<OrderDTO> GetById(int id);

        Task<List<OrderDTO>> GetByUserId(int userId);

        Task Create(OrderDTO orderDTO);

        Task Update(int id, OrderDTO orderDTO);

        Task DeleteById(int id);
    }
}
