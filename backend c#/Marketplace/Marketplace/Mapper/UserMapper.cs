using UserService.DTO;
using UserService.Entity;

namespace UserService.Mapper
{
    public class UserMapper
    {
        public static UserDTO MapUserToUserDTO(User user)
        {
            UserDTO userDTO = new UserDTO();
            userDTO.Login = user.Login;
            userDTO.Password = user.Password;
            userDTO.Email = user.Email;
            userDTO.Role = user.Role;
            return userDTO;
        }

        public static User MapUserDTOToUser(UserDTO userDTO)
        {
            User user = new User();
            user.Login = userDTO.Login;
            user.Password = userDTO.Password;
            user.Email = userDTO.Email;
            user.Role = userDTO.Role;
            return user;
        }
    }
}
