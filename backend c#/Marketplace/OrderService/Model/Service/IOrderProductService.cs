using OrderService.Model.DTO;

namespace OrderService.Model.Service
{
    public interface IOrderProductService
    {
        Task<List<OrderProductDTO>> GetAll();

        Task<OrderProductDTO?> GetById(int id);

        Task<List<OrderProductDTO>> GetByOrderId(int orderId);

        Task<List<OrderProductDTO>> GetByUserId(int userId);

        Task Create(OrderProductDTO orderProductDTO);

        Task Update(int id, OrderProductDTO orderProductDTO);

        Task DeleteById(int id);
    }
}
