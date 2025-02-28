using UserService.Db;
using UserService.Entity;
using UserService.Utils;

namespace UserService.Repository
{
    public class UserRepositoryImpl : IUserRepository
    {
        private UserContext _userContext;

        public UserRepositoryImpl(UserContext userContext) => _userContext = userContext;

        public void Create(User user)
        {
            user.Password = HashFunc.HashFunction(user.Password);
            _userContext.Users.Add(user);
            _userContext.SaveChanges();
        }

        public void DeleteById(int id)
        {
            User? currentUser = GetById(id);
            if (currentUser != null)
            {
                _userContext.Users.Remove(currentUser);
                _userContext.SaveChanges();
            }   
        }

        public List<User> GetAll()
        {
            return _userContext.Users.ToList();
        }

        public User? GetById(int id)
        {
            return (from user in _userContext.Users.ToList()
                    where user.Id == id
                    select user).FirstOrDefault();
        }

        public User? GetUserByLogin(string login)
        {
            return (from user in _userContext.Users.ToList()
                    where user.Login == login
                    select user).FirstOrDefault();
        }

        public void Update(int id, User user)
        {
            User? currentUser = GetById(id);

            if (currentUser != null)
            {
                currentUser.Login = user.Login;
                currentUser.Password = HashFunc.HashFunction(user.Password);
                currentUser.Email = user.Email;
                currentUser.Role = user.Role;
                _userContext.Users.Update(currentUser);
                _userContext.SaveChanges();
            }
        }
    }
}
