﻿using OrderService.Entity;

namespace OrderService.Repository
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAll();

        Task<Order?> GetById(int id);

        Task<List<Order>> GetByUserId(int userId);

        Task Create(Order order);

        Task Update(int id, Order order);

        Task DeleteById(int id);
    }
}
