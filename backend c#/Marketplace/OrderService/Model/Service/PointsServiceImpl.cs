using System.Net;
using System.Text;
using OrderService.Model.DTO;
using OrderService.Model.Entity;
using OrderService.Model.Mapper;
using OrderService.Model.Repository;

namespace OrderService.Model.Service
{
    public class PointsServiceImpl : IPointsService
    {
        private readonly IPointsRepository _pointsRepository;

        public PointsServiceImpl(IPointsRepository pointsRepository) => _pointsRepository = pointsRepository;
        public async Task Create(PointsDTO pointsDTO)
        {
            string token = GenerateRandomToken();
            pointsDTO.Token = token;
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

            if (points != null)
                return PointsMapper.MapPointsToPointsDTO(points);

            return new PointsDTO();
        }

        public async Task<PointsDTO?> GetById(int id)
        {
            Points? points = await _pointsRepository.GetById(id);

            if (points != null)
                return PointsMapper.MapPointsToPointsDTO(points);

            return null;
        }

        public async Task Update(int id, PointsDTO pointsDTO)
        {
            PointsDTO? currentPointsDTO = await GetById(id);

            if (currentPointsDTO != null)
                await _pointsRepository.Update(id, PointsMapper.MapPointsDTOToPoints(pointsDTO));
        }

        public async Task<PointsDTO?> GetByToken(string token)
        {
            Points? points = await _pointsRepository.GetByToken(token);

            if (points != null)
                return PointsMapper.MapPointsToPointsDTO(points);

            return null;
        }

        public string GenerateRandomToken()
        {
            int length = 9;
            const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder tokenBuilder = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(validChars.Length);
                tokenBuilder.Append(validChars[index]);
            }

            return tokenBuilder.ToString();
        }
    }
}
