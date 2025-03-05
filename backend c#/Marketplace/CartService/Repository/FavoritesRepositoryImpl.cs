using CartService.Db;
using CartService.Entity;
using Microsoft.EntityFrameworkCore;

namespace CartService.Repository
{
    public class FavoritesRepositoryImpl : IFavoritesRepository
    {
        private readonly CartContext _cartContext;

        public FavoritesRepositoryImpl(CartContext cartContext) => _cartContext = cartContext;

        public async Task Create(Favorites favorites)
        {
            await _cartContext.Favorites.AddAsync(favorites);
            await _cartContext.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            Favorites? favorites = await GetById(id);

            if(favorites != null)
            {
                _cartContext.Favorites.Remove(favorites);
                await _cartContext.SaveChangesAsync();
            }
        }

        public async Task<List<Favorites>> GetAll()
        {
            return await _cartContext.Favorites.ToListAsync();
        }

        public async Task<Favorites?> GetById(int id)
        {
            Favorites? favorites = await _cartContext.Favorites.FindAsync(id);

            if (favorites != null)
                return favorites;

            return null;
        }

        public async Task<List<Favorites>> GetByUserId(int userId)
        {
            return await _cartContext.Favorites
                            .Where(f => f.UserId == userId)
                            .ToListAsync();
        }

        public async Task<Favorites?> GetByUserIdAndProductId(int userId, int productId)
        {
            Favorites? favorites = await _cartContext.Favorites
                                .Where(f => f.UserId == userId && f.ProductId == productId)
                                .FirstOrDefaultAsync();

            if (favorites != null)
                return favorites;

            return new Favorites();
        }

        public async Task Update(int id, Favorites favorites)
        {
            Favorites? currentFavorites = await GetById(id);

            if (currentFavorites != null)
            {
                currentFavorites.ProductId = favorites.ProductId;
                currentFavorites.UserId = favorites.UserId;
                currentFavorites.DateAdded = favorites.DateAdded;
                _cartContext.Update(currentFavorites);
                await _cartContext.SaveChangesAsync();
            }
        }
    }
}
