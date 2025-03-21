using CartService.Model.DTO;
using CartService.Model.Entity;
using CartService.View;

namespace CartService.Model.Mapper
{
    public class FavoritesMapper
    {
        public static FavoritesDTO MapFavoritesToFavoritesDTO(Favorites favorites)
        {
            FavoritesDTO favoritesDTO = new FavoritesDTO();
            favoritesDTO.Id = favorites.Id;
            favoritesDTO.ProductId = favorites.ProductId;
            favoritesDTO.UserId = favorites.UserId;
            favoritesDTO.DateAdded = favorites.DateAdded;
            return favoritesDTO;
        }

        public static Favorites MapFavoritesDTOToFavorites(FavoritesDTO favoritesDTO)
        {
            Favorites favorites = new Favorites();
            favorites.ProductId = favoritesDTO.ProductId;
            favorites.UserId = favoritesDTO.UserId;
            favorites.DateAdded = favoritesDTO.DateAdded;
            return favorites;
        }

        public static FavoritesView MapFavoritesDTOToFavoritesView(FavoritesDTO favoritesDTO)
        {
            FavoritesView favoritesView = new FavoritesView();
            favoritesView.Id = favoritesDTO.Id;
            favoritesView.ProductId = favoritesDTO.ProductId;
            favoritesView.UserId = favoritesDTO.UserId;
            favoritesView.DateAdded = favoritesDTO.DateAdded;
            return favoritesView;
        }

        public static List<FavoritesView> MapFavortiesDTOListToFavoritesViewList(
            List<FavoritesDTO> favortiesDTOList)
        {
            return favortiesDTOList.Select(MapFavoritesDTOToFavoritesView).ToList();
        }
    }
}
