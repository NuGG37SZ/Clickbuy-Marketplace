using OrderService.Entity;

namespace OrderService.Repository
{
    public interface IOrderProductRepository
    {
        Task<List<OrderProduct>> GetAll();

        Task<OrderProduct?> GetById(int id);

        Task<List<OrderProduct>> GetByOrderId (int orderId);

        Task<List<OrderProduct>> GetByUserId(int userId);

        Task Create(OrderProduct orderProduct);  

        Task Update(int id, OrderProduct orderProduct);

        Task DeleteById(int id);
    }
}
