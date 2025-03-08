using OrderService.DTO;

namespace OrderService.Client
{
    public class CartClient
    {
        private readonly HttpClient _httpClient;

        public CartClient(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<CartDTO> GetCartById(int cartId)
        {
            using var response = await _httpClient.GetAsync($"https://localhost:7073/api/v1/carts/getById/{cartId}");
            CartDTO? cartDTO = await response.Content.ReadFromJsonAsync<CartDTO>();

            if (cartDTO != null) 
                return cartDTO;

            return null;
        }
    }
}
