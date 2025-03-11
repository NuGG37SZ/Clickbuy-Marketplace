using Microsoft.EntityFrameworkCore;
using OrderService.Db;
using OrderService.Entity;

namespace OrderService.Repository
{
    public class OrderRepositoryImpl : IOrderRepository
    {
        private readonly OrderContext _orderContext;

        public OrderRepositoryImpl(OrderContext orderContext) => _orderContext = orderContext;

        public async Task Create(Order order)
        {
            await _orderContext.Orders.AddAsync(order);
            await _orderContext.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            Order? order = await GetById(id);

            if (order != null)
            {
                _orderContext.Orders.Remove(order);
                await _orderContext.SaveChangesAsync();
            }
        }

        public async Task<List<Order>> GetAll()
        {
            return await _orderContext.Orders.ToListAsync();
        }

        public async Task<Order?> GetById(int id)
        {
            Order? order = await _orderContext.Orders.FindAsync(id);

            if(order != null) return order;

            return null;
        }

        public async Task<List<Order>> GetByUserId(int userId)
        {
            return await _orderContext.Orders
                            .Where(o => o.UserId == userId)
                            .ToListAsync();
        }

        public async Task Update(int id, Order order)
        {
            Order? currentOrder = await GetById(id);

            if (currentOrder != null)
            {
                currentOrder.UpdateOrder = order.UpdateOrder;
                currentOrder.CreateOrder = order.CreateOrder;
                currentOrder.UserId = order.UserId;
                currentOrder.Status = order.Status;
                _orderContext.Orders.Update(currentOrder);
                await _orderContext.SaveChangesAsync();
            }
        }
    }
}
