using CartService.DTO;

namespace CartService.Client
{
    public class ProductSizesClient
    {
        private readonly HttpClient _httpClient;

        public ProductSizesClient(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<ProductSizesDTO?> GetProductSizesById(int productSizesId)
        {
            using var response = await 
                _httpClient.GetAsync($"https://localhost:58841/api/v1/productSizes/getById/{productSizesId}");
            ProductSizesDTO? productSizesDTO = await response.Content.ReadFromJsonAsync<ProductSizesDTO>();

            if (productSizesDTO != null)
                return productSizesDTO;

            return null;
        }
    }
}
