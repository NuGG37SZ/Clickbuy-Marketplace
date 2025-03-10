using OrderService.DTO;

namespace OrderService.Service
{
    public interface IUserPointsService
    {
        Task<List<UserPointsDTO>> GetAll();

        Task<UserPointsDTO?> GetById(int id);

        Task<List<UserPointsDTO>> GetByUserId(int userId);

        Task Create(UserPointsDTO userPointsDTO);

        Task Update(int id, UserPointsDTO userPointsDTO);

        Task DeleteById(int id);
    }
}
