using UserService.Model.Entity;
using UserService.View.DTO;

namespace UserService.Model.Mapper
{
    public class UserMapper
    {
        public static UserDTO MapUserToUserDTO(User user)
        {
            UserDTO userDTO = new UserDTO();
            userDTO.Id = user.Id;
            userDTO.Login = user.Login;
            userDTO.Password = user.Password;
            userDTO.Email = user.Email;
            userDTO.Role = user.Role;
            userDTO.isBanned = user.isBanned;
            return userDTO;
        }

        public static User MapUserDTOToUser(UserDTO userDTO)
        {
            User user = new User();
            user.Login = userDTO.Login;
            user.Password = userDTO.Password;
            user.Email = userDTO.Email;
            user.Role = userDTO.Role;
            user.isBanned = userDTO.isBanned;
            return user;
        }
    }
}
