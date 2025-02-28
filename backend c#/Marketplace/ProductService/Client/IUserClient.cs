using ProductService.DTO;

namespace ProductService.Client
{
    public interface IUserClient
    {
        Task<UserDTO> GetUserById(int id);
    }
}
