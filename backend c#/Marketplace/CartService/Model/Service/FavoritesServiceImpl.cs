using CartService.Model.DTO;
using CartService.Model.Entity;
using CartService.Model.Mapper;
using CartService.Model.Repository;

namespace CartService.Model.Service
{
    public class FavoritesServiceImpl : IFavoritesService
    {
        private readonly IFavoritesRepository _favoritesRepository;

        public FavoritesServiceImpl(IFavoritesRepository favoritesRepository) => _favoritesRepository = favoritesRepository;

        public async Task Create(FavoritesDTO favoritesDTO)
        {
            await _favoritesRepository.Create(FavoritesMapper.MapFavoritesDTOToFavorites(favoritesDTO));
        }

        public async Task DeleteById(int id)
        {
            FavoritesDTO? favoritesDTO = await GetById(id);

            if (favoritesDTO != null)
                await _favoritesRepository.DeleteById(id);
        }

        public async Task<List<FavoritesDTO>> GetAll()
        {
            List<Favorites> favoritesList = await _favoritesRepository.GetAll();
            return favoritesList
                    .Select(FavoritesMapper.MapFavoritesToFavoritesDTO)
                    .ToList();

        }

        public async Task<FavoritesDTO?> GetById(int id)
        {
            return FavoritesMapper.MapFavoritesToFavoritesDTO(await _favoritesRepository.GetById(id));
        }

        public async Task<List<FavoritesDTO>> GetByUserId(int userId)
        {
            List<Favorites> favoritesList = await _favoritesRepository.GetByUserId(userId);
            return favoritesList
                    .Select(FavoritesMapper.MapFavoritesToFavoritesDTO)
                    .ToList();
        }

        public async Task<FavoritesDTO?> GetByUserIdAndProductId(int userId, int productId)
        {
            return FavoritesMapper.MapFavoritesToFavoritesDTO(
                    await _favoritesRepository.GetByUserIdAndProductId(userId, productId)
            );
        }

        public async Task Update(int id, FavoritesDTO favoritesDTO)
        {
            FavoritesDTO? currentFavoritesDTO = await GetById(id);

            if (currentFavoritesDTO != null)
            {
                currentFavoritesDTO.ProductId = favoritesDTO.ProductId;
                currentFavoritesDTO.UserId = favoritesDTO.UserId;
                currentFavoritesDTO.DateAdded = favoritesDTO.DateAdded;
                await _favoritesRepository.Update(id,
                    FavoritesMapper.MapFavoritesDTOToFavorites(currentFavoritesDTO)
                );
            }
        }
    }
}
