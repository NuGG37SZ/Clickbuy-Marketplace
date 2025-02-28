using ProductService.DTO;

namespace ProductService.HttpClient
{
    public class UserClient : IUserClient
    {
        private IHttpClientFactory _httpClientFactory;

        public UserClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<UserDTO> GetUserById(int id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            object? data = await httpClient.GetFromJsonAsync($"http://localhost:5098/users/{id}", typeof(UserDTO));
            if(data is UserDTO userDTO)
                return userDTO;

            return null;
        }
    }
}
