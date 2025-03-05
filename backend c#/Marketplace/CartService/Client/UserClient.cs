using CartService.DTO;

namespace CartService.Client
{
    public class UserClient
    {
        private readonly HttpClient _httpClient;

        public UserClient(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<UserDTO?> GetUserById(int userId)
        {
            using var response = await _httpClient.GetAsync($"http://localhost:5098/api/v1/users/{userId}");
            UserDTO? userDTO = await response.Content.ReadFromJsonAsync<UserDTO>();
            
            if(userDTO != null)
                return userDTO;

            return null;
        }
    }
}
