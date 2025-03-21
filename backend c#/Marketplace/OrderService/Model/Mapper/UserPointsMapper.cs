using OrderService.Model.DTO;
using OrderService.Model.Entity;
using OrderService.View;

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

        public static UserPointsView MapUserPointDTOToUserPointsView(UserPointsDTO userPointsDTO)
        {
            UserPointsView userPointsView = new UserPointsView();
            userPointsView.Id = userPointsDTO.Id;
            userPointsView.PointsId = userPointsDTO.PointsId;
            userPointsView.UserId = userPointsDTO.UserId;
            userPointsView.IsActive = userPointsDTO.IsActive;
            return userPointsView;
        }

        public static List<UserPointsView> MapUserPointDTOListToUserPointViewList(
            List<UserPointsDTO> userPointsDTOList)
        {
            return userPointsDTOList.Select(MapUserPointDTOToUserPointsView).ToList();
        }

    }
}
