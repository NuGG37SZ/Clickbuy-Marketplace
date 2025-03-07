using UserService.DTO;
using UserService.Entity;
using UserService.Mapper;
using UserService.Repository;

namespace UserService.Service
{
    public class UserServiceImpl : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserServiceImpl(IUserRepository userRepository) => _userRepository = userRepository;

        public async Task Create(UserDTO userDTO)
        {
            await _userRepository.Create(UserMapper.MapUserDTOToUser(userDTO));
        }

        public async Task DeleteById(int id)
        {
            UserDTO? userDTO = await GetById(id);

            if (userDTO != null)
                await _userRepository.DeleteById(id);

        }

        public async Task<List<UserDTO>> GetAll()
        {
            List<User> users = await _userRepository.GetAll();
            return users
                .Select(UserMapper.MapUserToUserDTO)
                .ToList();
        }

        public async Task<UserDTO?> GetById(int id)
        {
            return UserMapper.MapUserToUserDTO(await _userRepository.GetById(id));
        }

        public async Task<UserDTO?> GetUserByLogin(string login)
        {
            UserDTO? currentUser = UserMapper.MapUserToUserDTO(await _userRepository.GetUserByLogin(login));

            if (currentUser != null)
                return currentUser;

            return null;
        }

        public async Task Update(int id, UserDTO userDTO)
        {
            UserDTO? currentUser = await GetById(id);

            if (currentUser != null)
            {
                currentUser.Login = userDTO.Login;
                currentUser.Password = userDTO.Password;
                currentUser.Email = userDTO.Email;
                currentUser.Role = userDTO.Role;
                await _userRepository.Update(id, UserMapper.MapUserDTOToUser(currentUser));
            }
        }
    }
}
