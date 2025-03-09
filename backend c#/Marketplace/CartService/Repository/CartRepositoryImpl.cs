using CartService.Db;
using CartService.Entity;
using Microsoft.EntityFrameworkCore;

namespace CartService.Repository
{
    public class CartRepositoryImpl : ICartRepository
    {
        private readonly CartContext _cartContext;

        public CartRepositoryImpl(CartContext cartContext) => _cartContext = cartContext;

        public async Task Create(Cart cart)
        {
            await _cartContext.Carts.AddAsync(cart);
            await _cartContext.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            Cart? currentCart = await GetById(id);

            if (currentCart != null)
            {
                _cartContext.Carts.Remove(currentCart);
                await _cartContext.SaveChangesAsync();
            }
        }

        public async Task<List<Cart>> GetAll()
        {
            return await _cartContext.Carts.ToListAsync();
        }

        public async Task<Cart?> GetById(int id)
        {
            return await _cartContext.Carts.FindAsync(id);
        }

        public async Task<List<Cart>> GetByUserId(int userId)
        {
            return await _cartContext.Carts
                            .Where(cart => cart.UserId == userId)
                            .ToListAsync();
        }

        public async Task Update(int id, Cart cart)
        {
            Cart? currentCart = await GetById(id);

            if (currentCart != null)
            {
                currentCart.ProductId = cart.ProductId;
                currentCart.UserId = cart.UserId;
                currentCart.ProductSizesId = cart.ProductSizesId;
                currentCart.Count = cart.Count;
                _cartContext.Carts.Update(currentCart);
                await _cartContext.SaveChangesAsync();
            }
        }
    }
}
