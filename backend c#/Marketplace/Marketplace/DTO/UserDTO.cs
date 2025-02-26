namespace UserService.DTO
{
    public class UserDTO
    {
        public int Id { get; private set; }

        public string Login { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Role { get; set; } = null!;

    }
}
