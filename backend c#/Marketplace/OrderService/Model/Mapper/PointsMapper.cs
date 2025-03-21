using OrderService.Model.DTO;
using OrderService.Model.Entity;
using OrderService.View;

namespace OrderService.Model.Mapper
{
    public class PointsMapper
    {
        public static PointsDTO MapPointsToPointsDTO(Points points)
        {
            PointsDTO pointsDTO = new PointsDTO();
            pointsDTO.Id = points.Id;
            pointsDTO.Address = points.Address;
            pointsDTO.Token = points.Token;
            return pointsDTO;
        }

        public static Points MapPointsDTOToPoints(PointsDTO pointsDTO)
        {
            Points points = new Points();
            points.Address = pointsDTO.Address;
            points.Token = pointsDTO.Token;
            return points;
        }

        public static PointsView MapPointsDTOToPointsView(PointsDTO pointsDTO)
        {
            PointsView pointsView = new PointsView();
            pointsView.Id = pointsDTO.Id;
            pointsView.Address = pointsDTO.Address;
            pointsView.Token = pointsDTO.Token;
            return pointsView;
        }

        public static List<PointsView> MapPointsDTOListToPointsViewList(List<PointsDTO> pointsDTOs)
        {
            return pointsDTOs.Select(MapPointsDTOToPointsView).ToList();
        }
    }
}
