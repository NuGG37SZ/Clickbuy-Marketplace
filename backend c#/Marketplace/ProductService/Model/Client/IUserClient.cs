using ProductService.View.DTO;

namespace ProductService.Model.Client
{
    public interface IUserClient
    {
        Task<UserDTO> GetUserById(int id);
    }
}
