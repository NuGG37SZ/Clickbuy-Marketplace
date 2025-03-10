using OrderService.Entity;

namespace OrderService.Repository
{
    public interface IUserPointsRepository
    {
        Task<List<UserPoints>> GetAll();

        Task<UserPoints?> GetById(int id);

        Task<List<UserPoints>> GetByUserId(int userId);
        
        Task Create(UserPoints userPoints);

        Task Update(int id, UserPoints userPoints);

        Task DeleteById(int id);
    }
}
