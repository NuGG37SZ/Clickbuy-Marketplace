using Microsoft.AspNetCore.Mvc;
using UserService.DTO;
using UserService.Service;

namespace Marketplace.Controllers
{
    [ApiController]
    [Route("/api/v1")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("/users")]
        public IActionResult GetAll()
        {
            List<UserDTO> users = _userService.GetAll();

            if (users == null)
            {
                return NotFound("No users found");
            }
 
            return Ok(users);
        }
    }
}
