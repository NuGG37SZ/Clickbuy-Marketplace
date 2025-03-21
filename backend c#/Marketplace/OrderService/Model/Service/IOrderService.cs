﻿using OrderService.Model.DTO;

namespace OrderService.Model.Service
{
    public interface IOrderService
    {
        Task<List<OrderDTO>> GetAll();

        Task<OrderDTO?> GetById(int id);

        Task<List<OrderDTO>> GetByUserId(int userId);

        Task<List<OrderDTO>> GetByOrderStatusAndUserId(string status, int userId);

        Task Create(OrderDTO orderDTO);

        Task Update(int id, OrderDTO orderDTO);

        Task DeleteById(int id);
    }
}
