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

        public void Update(int id, UserDTO userDTO)
        {
            User currentUser = UserMapper.MapUserDTOToUser(userDTO); 
            _userRepository.Update(id, currentUser);
        }
    }
}
