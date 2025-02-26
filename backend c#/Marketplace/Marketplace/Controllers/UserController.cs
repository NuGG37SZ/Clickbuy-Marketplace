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
        [Route("users")]
        public IActionResult GetAll()
        {
            List<UserDTO> users = _userService.GetAll();

            if (users == null)
            {
                return NotFound("No users found");
            }
 
            return Ok(users);
        }

        [HttpGet]
        [Route("users/{id}")]
        public IActionResult GetById(int id)
        {
            UserDTO? userDto = _userService.GetById(id);
            if (userDto == null)
            {
                return NotFound("User not found.");
            }
            return Ok(userDto);
        }

        [HttpPost]
        [Route("users/create")]
        public IActionResult Create([FromBody] UserDTO userDTO)
        {
            _userService.Create(userDTO);
            return Created("create", userDTO);
        }

        [HttpPut]
        [Route("users/update/{id}")]
        public IActionResult Update(int id, [FromBody] UserDTO userDTO)
        {
            UserDTO? currentUser = _userService.GetById(id);

            if (currentUser != null)
            {
                _userService.Update(id, userDTO);
                return Ok(currentUser);
            } 
           
            return NotFound("User Not Found!");
        }

        [HttpDelete]
        [Route("users/delete/{id}")]
        public IActionResult DeleteById(int id)
        {
            _userService.DeleteById(id);
            return NoContent();
        }
    }
}
