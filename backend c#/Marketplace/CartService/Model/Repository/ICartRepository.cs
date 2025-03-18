using CartService.Model.Entity;

namespace CartService.Model.Repository
{
    public interface ICartRepository
    {
        Task<List<Cart>> GetAll();

        Task<Cart?> GetById(int id);

        Task<List<Cart>> GetByUserId(int userId);

        Task Create(Cart cart);

        Task Update(int id, Cart cart);

        Task DeleteById(int id);

        Task DeleteRange(List<Cart> carts);
    }
}
