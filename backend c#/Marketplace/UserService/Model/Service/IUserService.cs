using UserService.Model.DTO;

namespace UserService.Model.Service
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAll();

        Task<UserDTO?> GetById(int id);

        Task Create(UserDTO userDTO);

        Task Update(int id, UserDTO userDTO);

        Task DeleteById(int id);

        Task<UserDTO?> GetUserByLogin(string login);
    }
}
