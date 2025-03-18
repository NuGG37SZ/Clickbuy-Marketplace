using OrderService.View.DTO;

namespace OrderService.Model.Service
{
    public interface IUserPointsService
    {
        Task<List<UserPointsDTO>> GetAll();

        Task<UserPointsDTO?> GetById(int id);

        Task<UserPointsDTO?> GetByUserIdAndPointsId(int userId, int pointsId);

        Task<List<UserPointsDTO>> GetByUserId(int userId);

        Task<UserPointsDTO?> GetByIsActiveAndUserId(bool isActive, int userId);

        Task Create(UserPointsDTO userPointsDTO);

        Task Update(int id, UserPointsDTO userPointsDTO);

        Task DeleteById(int id);
    }
}
