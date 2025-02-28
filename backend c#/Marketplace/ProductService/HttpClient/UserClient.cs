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

        public UserDTO GetUserById(int id)
        {
            
            var httpClient = _httpClientFactory.CreateClient();

        }
    }
}
