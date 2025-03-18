using Microsoft.EntityFrameworkCore;
using UserService.Model.Db;
using UserService.Model.Entity;
using UserService.Model.Utils;

namespace UserService.Model.Repository
{
    public class UserRepositoryImpl : IUserRepository
    {
        private UserContext _userContext;

        public UserRepositoryImpl(UserContext userContext) => _userContext = userContext;

        public async Task Create(User user)
        {
            user.Password = HashFunc.HashFunction(user.Password);
            await _userContext.Users.AddAsync(user);
            await _userContext.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            User? currentUser = await GetById(id);

            if (currentUser != null)
            {
                _userContext.Users.Remove(currentUser);
                _userContext.SaveChanges();
            }
        }

        public async Task<List<User>> GetAll()
        {
            return await _userContext.Users.ToListAsync();
        }

        public async Task<User?> GetById(int id)
        {
            return await _userContext.Users.FindAsync(id);
        }

        public async Task<User?> GetUserByLogin(string login)
        {
            return await _userContext.Users
                            .Where(u => u.Login == login)
                            .FirstOrDefaultAsync();
        }

        public async Task Update(int id, User user)
        {
            User? currentUser = await GetById(id);

            if (currentUser != null)
            {
                currentUser.Login = user.Login;
                currentUser.Password = user.Password;
                currentUser.Email = user.Email;
                currentUser.Role = user.Role;
                currentUser.isBanned = user.isBanned;
                _userContext.Users.Update(currentUser);
                await _userContext.SaveChangesAsync();
            }
        }
    }
}
