using Microsoft.EntityFrameworkCore;
using OrderService.Model.Db;
using OrderService.Model.Entity;

namespace OrderService.Model.Repository
{
    public class UserPointsRepositoryImpl : IUserPointsRepository
    {
        private readonly OrderContext _orderContext;

        public UserPointsRepositoryImpl(OrderContext orderContext) => _orderContext = orderContext;

        public async Task Create(UserPoints userPoints)
        {
            await _orderContext.UserPoints.AddAsync(userPoints);
            await _orderContext.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            UserPoints? userPoints = await GetById(id);

            if (userPoints != null)
            {
                _orderContext.UserPoints.Remove(userPoints);
                await _orderContext.SaveChangesAsync();
            }
        }

        public async Task<List<UserPoints>> GetAll()
        {
            return await _orderContext.UserPoints.ToListAsync();
        }

        public async Task<UserPoints?> GetById(int id)
        {
            return await _orderContext.UserPoints.FindAsync(id);
        }

        public async Task<UserPoints?> GetByIsActiveAndUserId(bool isActive, int userId)
        {
            return await _orderContext.UserPoints
                            .Where(up => up.IsActive == isActive && up.UserId == userId)
                            .FirstOrDefaultAsync();
        }

        public async Task<List<UserPoints>> GetByUserId(int userId)
        {
            List<UserPoints> userPoints = await _orderContext.UserPoints
                                        .Where(up => up.UserId == userId)
                                        .ToListAsync();
            return userPoints;
        }

        public async Task<UserPoints?> GetByUserIdAndPointsId(int userId, int pointsId)
        {
            return await _orderContext.UserPoints
                            .Where(us => us.UserId == userId && us.PointsId == pointsId)
                            .FirstOrDefaultAsync();
        }

        public async Task Update(int id, UserPoints userPoints)
        {
            UserPoints? currentUserPoints = await GetById(id);

            if (currentUserPoints != null)
            {
                currentUserPoints.UserId = userPoints.UserId;
                currentUserPoints.PointsId = userPoints.PointsId;
                currentUserPoints.IsActive = userPoints.IsActive;
                _orderContext.UserPoints.Update(currentUserPoints);
                await _orderContext.SaveChangesAsync();
            }
        }
    }
}
