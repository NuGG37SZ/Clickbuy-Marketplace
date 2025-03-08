using Microsoft.EntityFrameworkCore;
using OrderService.Db;
using OrderService.Entity;

namespace OrderService.Repository
{
    public class OrderProductRepositoryImpl : IOrderProductRepository
    {
        private readonly OrderContext _orderContext;

        public OrderProductRepositoryImpl(OrderContext orderContext) => _orderContext = orderContext;

        public async Task Create(OrderProduct orderProduct)
        {
            await _orderContext.OrderProducts.AddAsync(orderProduct);
            await _orderContext.SaveChangesAsync(); 
        }

        public async Task DeleteById(int id)
        {
            OrderProduct? orderProduct = await GetById(id);

            if(orderProduct != null)
            {
                _orderContext.OrderProducts.Remove(orderProduct);
                await _orderContext.SaveChangesAsync();
            }

        }

        public async Task<List<OrderProduct>> GetAll()
        {
            return await _orderContext.OrderProducts.ToListAsync();
        }

        public async Task<OrderProduct?> GetById(int id)
        {
            return await _orderContext.OrderProducts.FindAsync(id); 
        }

        public async Task Update(int id, OrderProduct orderProduct)
        {
            OrderProduct? currentOrderProduct = await GetById(id);

            if (currentOrderProduct != null)
            {
                currentOrderProduct.OrderId = orderProduct.OrderId;
                currentOrderProduct.CartId = orderProduct.CartId;
                _orderContext.Update(currentOrderProduct);
                await _orderContext.SaveChangesAsync();
            }
        }
    }
}
