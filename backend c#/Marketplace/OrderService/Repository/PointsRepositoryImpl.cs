using Microsoft.EntityFrameworkCore;
using OrderService.Db;
using OrderService.Entity;

namespace OrderService.Repository
{
    public class PointsRepositoryImpl : IPointsRepository
    {
        private readonly OrderContext _orderContext;

        public PointsRepositoryImpl(OrderContext orderContext) => _orderContext = orderContext;

        public async Task Create(Points points)
        {
            await _orderContext.Points.AddAsync(points);
            await _orderContext.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            Points? points = await GetById(id);

            if(points != null) 
            {
                _orderContext.Points.Remove(points);
                await _orderContext.SaveChangesAsync();
            }  
        }

        public async Task<List<Points>> GetAll()
        {
            return await _orderContext.Points.ToListAsync();
        }

        public async Task<Points?> GetById(int id)
        {
            return await _orderContext.Points.FindAsync(id);
        }

        public async Task Update(int id, Points points)
        {
            Points? currentPoints = await GetById(id);

            if (currentPoints != null)
            {
                currentPoints.Address = points.Address;
                _orderContext.Update(currentPoints);
                await _orderContext.SaveChangesAsync();
            }
        }
    }
}
