using UserService.Model.DTO;
using UserService.Model.Entity;
using UserService.View;

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

        public static UserView MapUserDTOToUserView(UserDTO userDTO)
        {
            UserView userView = new UserView();
            userView.Id = userDTO.Id;
            userView.Login = userDTO.Login;
            userView.Password = userDTO.Password;
            userView.Email = userDTO.Email;
            userView.Role = userDTO.Role;
            userView.isBanned = userDTO.isBanned;
            return userView;
        }

        public static List<UserView> MapListUserDTOToListUserView(List<UserDTO> userDTOList)
        {
            return userDTOList.Select(MapUserDTOToUserView).ToList();
        }
    }
}
