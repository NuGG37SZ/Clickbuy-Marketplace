using UserService.Db;
using UserService.DTO;
using UserService.Entity;
using UserService.Mapper;
using UserService.Repository;
using UserService.Utils;

namespace UserService.Service
{
    public class UserServiceImpl : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserServiceImpl(IUserRepository userRepository) => _userRepository = userRepository;

        public void Create(UserDTO userDTO)
        {
            _userRepository.Create(UserMapper.MapUserDTOToUser(userDTO));
        }

        public void DeleteById(int id)
        {
            _userRepository.DeleteById(id);
        }

        public List<UserDTO> GetAll()
        {
            return _userRepository.GetAll()
                .Select(UserMapper.MapUserToUserDTO)
                .ToList();
        }

        public UserDTO? GetById(int id)
        {
            return UserMapper.MapUserToUserDTO(_userRepository.GetById(id));
        }

        public UserDTO? GetUserByLogin(string login)
        {
            UserDTO currentUser = UserMapper.MapUserToUserDTO(_userRepository.GetUserByLogin(login));

            if (currentUser != null)
                return currentUser;

            return null;
        }

        public void Update(int id, UserDTO userDTO)
        {
            UserDTO? currentUser = GetById(id);

            if (currentUser != null)
            {
                currentUser.Login = userDTO.Login;
                currentUser.Password = userDTO.Password;
                currentUser.Email = userDTO.Email;
                currentUser.Role = userDTO.Role;
                _userRepository.Update(id, UserMapper.MapUserDTOToUser(currentUser));
            }
        }
    }
}
