using CartService.View.DTO;

namespace CartService.Client
{
    public class ProductClient
    {
        private readonly HttpClient _httpClient;

        public ProductClient(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<ProductDTO?> GetProductById(int productId)
        {
            using var response = 
                await _httpClient.GetAsync($"https://localhost:58841/api/v1/products/getById/{productId}");
            ProductDTO? productDTO = await response.Content.ReadFromJsonAsync<ProductDTO>();

            if( productDTO != null ) 
                return productDTO;

            return null;
        }
    }
}
