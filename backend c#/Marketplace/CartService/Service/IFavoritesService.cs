using CartService.DTO;

namespace CartService.Service
{
    public interface IFavoritesService
    {
        Task<List<FavoritesDTO>> GetAll();

        Task<FavoritesDTO?> GetById(int id);

        Task<List<FavoritesDTO>> GetByUserId(int userId);

        Task<FavoritesDTO?> GetByUserIdAndProductId(int userId, int productId);

        Task Create(FavoritesDTO favoritesDTO);

        Task Update(int id, FavoritesDTO favoritesDTO);

        Task DeleteById(int id);
    }
}
