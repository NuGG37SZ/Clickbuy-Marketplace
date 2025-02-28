using ProductService.DTO;

namespace ProductService.HttpClient
{
    public interface IUserClient
    {
        UserDTO GetUserById(int id);
    }
}
