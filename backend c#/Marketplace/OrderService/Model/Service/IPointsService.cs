using OrderService.View.DTO;

namespace OrderService.Model.Service
{
    public interface IPointsService
    {
        Task<List<PointsDTO>> GetAll();

        Task<PointsDTO?> GetById(int id);

        Task<PointsDTO?> GetByAddress(string address);

        Task<PointsDTO?> GetByToken(string token);

        Task Create(PointsDTO pointsDTO);

        Task Update(int id, PointsDTO pointsDTO);

        Task DeleteById(int id);
    }
}
