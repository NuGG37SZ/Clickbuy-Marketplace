using OrderService.DTO;

namespace OrderService.Service
{
    public interface IOrderProductService
    {
        Task<List<OrderProductDTO>> GetAll();

        Task<OrderProductDTO?> GetById(int id);

        Task Create(OrderProductDTO orderProductDTO); 

        Task Update(int id, OrderProductDTO orderProductDTO);

        Task DeleteById(int id);
    }
}
