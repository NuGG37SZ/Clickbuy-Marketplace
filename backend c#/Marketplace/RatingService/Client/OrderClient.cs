using RatingService.DTO;

namespace RatingService.Client
{
    public class OrderClient
    {
        private readonly HttpClient _httpClient;

        public OrderClient(HttpClient httpClient) => _httpClient = httpClient;
        
        public async Task<OrderDTO?> GetOrderById(int orderId)
        {
            using HttpResponseMessage response =
                await _httpClient.GetAsync($"https://localhost:7049/api/v1/orders/getById/{orderId}");
            OrderDTO? orderDTO = await response.Content.ReadFromJsonAsync<OrderDTO>();

            if( orderDTO != null )
                return orderDTO;

            return null;
        } 

    }
}
