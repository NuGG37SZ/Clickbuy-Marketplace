﻿using OrderService.Entity;

namespace OrderService.Repository
{
    public interface IPointsRepository
    {
        Task<List<Points>> GetAll();

        Task<Points?> GetById(int id);

        Task Create(Points points);

        Task Update(int id, Points points);

        Task DeleteById(int id);
    }
}
