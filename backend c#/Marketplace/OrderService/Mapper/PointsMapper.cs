﻿using OrderService.DTO;
using OrderService.Entity;

namespace OrderService.Mapper
{
    public class PointsMapper
    {
        public static PointsDTO MapPointsToPointsDTO(Points points)
        {
            PointsDTO pointsDTO = new PointsDTO();
            pointsDTO.Id = points.Id;
            pointsDTO.Address = points.Address;
            return pointsDTO;
        }

        public static Points MapPointsDTOToPoints(PointsDTO pointsDTO)
        {
            Points points = new Points();
            points.Address = pointsDTO.Address;
            return points;
        }
    }
}
