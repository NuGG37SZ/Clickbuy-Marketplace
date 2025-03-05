using CartService.DTO;

namespace CartService.Service
{
    public interface ICartService
    {
        Task<List<CartDTO>> GetAll();

        Task<CartDTO> GetById(int id);

        Task<List<CartDTO>> GetByUserId(int userId);

        Task Create(CartDTO cartDTO);

        Task Update(int id, CartDTO cartDTO);

        Task DeleteById(int id);
    }
}
