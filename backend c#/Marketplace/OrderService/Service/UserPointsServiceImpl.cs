using OrderService.DTO;
using OrderService.Entity;
using OrderService.Mapper;
using OrderService.Repository;

namespace OrderService.Service
{
    public class UserPointsServiceImpl : IUserPointsService
    {
        private readonly IUserPointsRepository _userPointsRepository;

        public UserPointsServiceImpl(IUserPointsRepository userPointsRepository) => 
            _userPointsRepository = userPointsRepository;

        public async Task Create(UserPointsDTO userPointsDTO)
        {
            await _userPointsRepository.Create(UserPointsMapper.MapUserPointsDTOToUserPoint(userPointsDTO));
        }

        public async Task DeleteById(int id)
        {
            await _userPointsRepository.DeleteById(id);
        }

        public async Task<List<UserPointsDTO>> GetAll()
        {
            List<UserPoints> userPoints = await _userPointsRepository.GetAll();

            return userPoints
                    .Select(UserPointsMapper.MapUserPointsToUserPointsDTO)
                    .ToList();
        }

        public async Task<UserPointsDTO?> GetById(int id)
        {
            UserPoints? userPoints = await _userPointsRepository.GetById(id);
            
            if(userPoints != null)
                return UserPointsMapper.MapUserPointsToUserPointsDTO(userPoints);

            return null;
        }

        public async Task<UserPointsDTO?> GetByIsActive(bool isActive)
        {
            UserPoints? userPoints = await _userPointsRepository.GetByIsActive(isActive);

            if (userPoints != null)
                return UserPointsMapper.MapUserPointsToUserPointsDTO(userPoints);

            return null;
        }

        public async Task<List<UserPointsDTO>> GetByUserId(int userId)
        {
            List<UserPoints> userPoints = await _userPointsRepository.GetByUserId(userId);

            return userPoints
                      .Select(UserPointsMapper.MapUserPointsToUserPointsDTO)
                      .ToList();
        }

        public async Task<UserPointsDTO?> GetByUserIdAndPointsId(int userId, int pointsId)
        {
            UserPoints? userPoint = await _userPointsRepository.GetByUserIdAndPointsId(userId, pointsId);

            if (userPoint != null)
                return UserPointsMapper.MapUserPointsToUserPointsDTO(userPoint);

            return null;
        }

        public async Task Update(int id, UserPointsDTO userPointsDTO)
        {
            UserPointsDTO? currentUserPoints = await GetById(id);

            if (currentUserPoints != null)
                await _userPointsRepository.Update(id, UserPointsMapper.MapUserPointsDTOToUserPoint(userPointsDTO));
        }
    }
}
