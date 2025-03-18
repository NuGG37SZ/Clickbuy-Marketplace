using OrderService.Model.Entity;

namespace OrderService.Model.Repository
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAll();

        Task<Order?> GetById(int id);

        Task<List<Order>> GetByUserId(int userId);

        Task<List<Order>> GetByOrderStatusAndUserId(string status, int userId);

        Task Create(Order order);

        Task Update(int id, Order order);

        Task DeleteById(int id);
    }
}
