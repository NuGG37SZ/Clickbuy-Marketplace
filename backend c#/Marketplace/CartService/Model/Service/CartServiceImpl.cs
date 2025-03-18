using CartService.Model.Entity;
using CartService.Model.Mapper;
using CartService.Model.Repository;
using CartService.View.DTO;

namespace CartService.Model.Service
{
    public class CartServiceImpl : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartServiceImpl(ICartRepository cartRepository) => _cartRepository = cartRepository;

        public async Task Create(CartDTO cartDTO)
        {
            await _cartRepository.Create(CartMapper.MapCartDTOToCart(cartDTO));
        }

        public async Task DeleteById(int id)
        {
            CartDTO currentCartDTO = await GetById(id);

            if (currentCartDTO != null)
                await _cartRepository.DeleteById(id);
        }

        public async Task DeleteRange(List<CartDTO> cartDTOs)
        {
            List<Cart> carts = cartDTOs
                    .Select(CartMapper.MapCartDTOToCart)
                    .ToList();
            await _cartRepository.DeleteRange(carts);
        }

        public async Task<List<CartDTO>> GetAll()
        {
            List<Cart> carts = await _cartRepository.GetAll();
            return carts
                    .Select(CartMapper.MapCartToCartDTO)
                    .ToList();
        }

        public async Task<CartDTO> GetById(int id)
        {
            Cart? cart = await _cartRepository.GetById(id);

            if (cart != null)
                return CartMapper.MapCartToCartDTO(cart);

            return null;
        }

        public async Task<List<CartDTO>> GetByUserId(int userId)
        {
            List<Cart> carts = await _cartRepository.GetByUserId(userId);
            return carts
                .Select(CartMapper.MapCartToCartDTO)
                .ToList();
        }

        public async Task Update(int id, CartDTO cartDTO)
        {
            CartDTO currentCartDTO = await GetById(id);

            if (currentCartDTO != null)
            {
                currentCartDTO.ProductId = cartDTO.ProductId;
                currentCartDTO.UserId = cartDTO.UserId;
                currentCartDTO.ProductSizesId = cartDTO.ProductSizesId;
                currentCartDTO.Count = cartDTO.Count;
                await _cartRepository.Update(id, CartMapper.MapCartDTOToCart(currentCartDTO));
            }
        }
    }
}
