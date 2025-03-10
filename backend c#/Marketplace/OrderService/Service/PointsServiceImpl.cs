using OrderService.DTO;
using OrderService.Entity;
using OrderService.Mapper;
using OrderService.Repository;

namespace OrderService.Service
{
    public class PointsServiceImpl : IPointsService
    {
        private readonly IPointsRepository _pointsRepository;

        public PointsServiceImpl(IPointsRepository pointsRepository) => _pointsRepository = pointsRepository;
        public async Task Create(PointsDTO pointsDTO)
        {
            await _pointsRepository.Create(PointsMapper.MapPointsDTOToPoints(pointsDTO));
        }

        public async Task DeleteById(int id)
        {
            await _pointsRepository.DeleteById(id);
        }

        public async Task<List<PointsDTO>> GetAll()
        {
            List<Points> points = await _pointsRepository.GetAll();

            return points
                     .Select(PointsMapper.MapPointsToPointsDTO)
                     .ToList();
        }

        public async Task<PointsDTO?> GetByAddress(string address)
        {
            Points? points = await _pointsRepository.GetByAddress(address);

            if(points != null)
                return PointsMapper.MapPointsToPointsDTO(points);

            return null;
        }

        public async Task<PointsDTO?> GetById(int id)
        {
            Points? points = await _pointsRepository.GetById(id);

            if(points != null)
                return PointsMapper.MapPointsToPointsDTO(points);

            return null;
        }

        public async Task Update(int id, PointsDTO pointsDTO)
        {
            PointsDTO? currentPointsDTO = await GetById(id);

            if (currentPointsDTO != null)
                await _pointsRepository.Update(id, PointsMapper.MapPointsDTOToPoints(pointsDTO));
        }
    }
}
