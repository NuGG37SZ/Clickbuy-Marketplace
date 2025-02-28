using UserService.DTO;
using UserService.Entity;

namespace UserService.Service
{
    public interface IUserService
    {
        List<UserDTO> GetAll();

        UserDTO? GetById(int id);

        void Create(UserDTO userDTO);

        void Update(int id, UserDTO userDTO);

        void DeleteById(int id);

        UserDTO? GetUserByLogin(string login);
    }
}
