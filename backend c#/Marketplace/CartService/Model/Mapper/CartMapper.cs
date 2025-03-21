using CartService.Model.DTO;
using CartService.Model.Entity;
using CartService.View;

namespace CartService.Model.Mapper
{
    public class CartMapper
    {
        public static CartDTO MapCartToCartDTO(Cart cart)
        {
            CartDTO cartDTO = new CartDTO();
            cartDTO.Id = cart.Id;
            cartDTO.ProductId = cart.ProductId;
            cartDTO.UserId = cart.UserId;
            cartDTO.ProductSizesId = cart.ProductSizesId;
            cartDTO.Count = cart.Count;
            return cartDTO;
        }

        public static Cart MapCartDTOToCart(CartDTO cartDTO)
        {
            Cart cart = new Cart();
            cart.ProductId = cartDTO.ProductId;
            cart.UserId = cartDTO.UserId;
            cart.ProductSizesId = cartDTO.ProductSizesId;
            cart.Count = cartDTO.Count;
            return cart;
        }

        public static CartView MapCartDTOToCartView(CartDTO cartDTO)
        {
            CartView cartView = new CartView();
            cartView.Id = cartDTO.Id;
            cartView.ProductId = cartDTO.ProductId;
            cartView.UserId = cartDTO.UserId;
            cartView.ProductSizesId = cartDTO.ProductSizesId;
            cartView.Count = cartDTO.Count;
            return cartView;
        }

        public static List<CartView> MapCartDTOListToCartViewList(List<CartDTO> cartDTOs)
        {
            return cartDTOs.Select(MapCartDTOToCartView).ToList();
        }
    }
}
