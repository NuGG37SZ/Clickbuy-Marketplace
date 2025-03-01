using System.Net.Http;
using ProductService.DTO;

namespace ProductService.Client
{
    public class UserClient : IUserClient
    {
        private HttpClient _httpClient;

        public UserClient(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<UserDTO> GetUserById(int id)
        {
            object? data = await _httpClient.GetFromJsonAsync($"http://localhost:5098/api/v1/users/{id}",
                typeof(UserDTO));

            if(data is UserDTO userDTO)
                return userDTO;

            return null;
        }
    }
}
