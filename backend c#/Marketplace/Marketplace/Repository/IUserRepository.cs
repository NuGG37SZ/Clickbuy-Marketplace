using UserService.Entity;

namespace UserService.Repository
{
    public interface IUserRepository
    {
        List<User> GetAll();

        User? GetById(int id);

        void Create(User user);

        void Update(int id, User user);

        void DeleteById(int id);
    }
}
