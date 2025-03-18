using OrderService.Model.Entity;
using OrderService.View.DTO;

namespace OrderService.Model.Mapper
{
    public class UserPointsMapper
    {
        public static UserPointsDTO MapUserPointsToUserPointsDTO(UserPoints userPoints)
        {
            UserPointsDTO userPointsDTO = new UserPointsDTO();
            userPointsDTO.Id = userPoints.Id;
            userPointsDTO.PointsId = userPoints.PointsId;
            userPointsDTO.UserId = userPoints.UserId;
            userPointsDTO.IsActive = userPoints.IsActive;
            return userPointsDTO;
        }

        public static UserPoints MapUserPointsDTOToUserPoint(UserPointsDTO userPointsDTO)
        {
            UserPoints userPoints = new UserPoints();
            userPoints.PointsId = userPointsDTO.PointsId;
            userPoints.UserId = userPointsDTO.UserId;
            userPoints.IsActive = userPointsDTO.IsActive;
            return userPoints;
        }

    }
}
