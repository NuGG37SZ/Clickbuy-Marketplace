using UserService.Model.DTO;
using UserService.Model.Entity;
using UserService.Model.Mapper;
using UserService.Model.Repository;

namespace UserService.Model.Service
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
            User? currentUser = await _userRepository.GetById(id);

            if (currentUser != null)
                return UserMapper.MapUserToUserDTO(currentUser);

            return null;
        }

        public async Task<UserDTO?> GetUserByLogin(string login)
        {
            User? currentUser = await _userRepository.GetUserByLogin(login);

            if (currentUser != null)
                return UserMapper.MapUserToUserDTO(currentUser);

            return null;
        }

        public async Task Update(int id, UserDTO userDTO)
        {
            UserDTO? currentUser = await GetById(id);

            if (currentUser != null)
                await _userRepository.Update(id, UserMapper.MapUserDTOToUser(userDTO));
        }
    }
}
