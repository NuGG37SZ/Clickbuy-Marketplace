using Microsoft.EntityFrameworkCore;
using OrderService.Model.Db;
using OrderService.Model.Entity;

namespace OrderService.Model.Repository
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

            if (points != null)
            {
                _orderContext.Points.Remove(points);
                await _orderContext.SaveChangesAsync();
            }
        }

        public async Task<List<Points>> GetAll()
        {
            return await _orderContext.Points.ToListAsync();
        }

        public async Task<Points?> GetByAddress(string address)
        {
            return await _orderContext.Points
                            .Where(p => p.Address == address)
                            .FirstOrDefaultAsync();
        }

        public async Task<Points?> GetById(int id)
        {
            return await _orderContext.Points.FindAsync(id);
        }

        public async Task<Points?> GetByToken(string token)
        {
            return await _orderContext.Points
                            .Where(p => p.Token.Equals(token))
                            .FirstOrDefaultAsync();
        }

        public async Task Update(int id, Points points)
        {
            Points? currentPoints = await GetById(id);

            if (currentPoints != null)
            {
                currentPoints.Address = points.Address;
                currentPoints.Token = points.Token;
                _orderContext.Update(currentPoints);
                await _orderContext.SaveChangesAsync();
            }
        }
    }
}
