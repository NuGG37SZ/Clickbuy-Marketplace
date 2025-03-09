using CartService.DTO;
using CartService.Entity;

namespace CartService.Mapper
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
    }
}
