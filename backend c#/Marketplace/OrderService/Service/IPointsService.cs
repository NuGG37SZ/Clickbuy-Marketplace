using OrderService.DTO;

namespace OrderService.Service
{
    public interface IPointsService
    {
        Task<List<PointsDTO>> GetAll();

        Task<PointsDTO?> GetById(int id);

        Task Create(PointsDTO pointsDTO);

        Task Update(int id, PointsDTO pointsDTO);

        Task DeleteById(int id);
    }
}
