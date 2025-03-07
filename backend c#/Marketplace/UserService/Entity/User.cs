using System.ComponentModel.DataAnnotations;

namespace UserService.Entity
{
    public class User
    {
        
        public int Id { get; set; }

        [Required]
        public string Login { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        public string Role { get; set; } = null!;
    }
}
