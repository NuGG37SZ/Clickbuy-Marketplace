using CartService.Entity;

namespace CartService.Repository
{
    public interface IFavoritesRepository
    {
        Task<List<Favorites>> GetAll();

        Task<Favorites?> GetById(int id);

        Task<List<Favorites>> GetByUserId(int userId);

        Task<Favorites?> GetByUserIdAndProductId(int userId, int productId);

        Task Create(Favorites favorites);

        Task Update(int id, Favorites favorites);

        Task DeleteById(int id);
    }
}
