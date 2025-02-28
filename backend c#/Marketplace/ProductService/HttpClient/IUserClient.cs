using ProductService.DTO;

namespace ProductService.HttpClient
{
    public interface IUserClient
    {
        Task<UserDTO> GetUserById(int id);
    }
}
