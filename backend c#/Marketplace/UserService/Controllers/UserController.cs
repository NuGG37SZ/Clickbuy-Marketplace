using Microsoft.AspNetCore.Mvc;
using UserService.Model.Service;
using UserService.Model.Utils;
using UserService.View.DTO;

namespace Marketplace.Controllers
{
    [ApiController]
    [Route("/api/v1/users")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) => _userService = userService;


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _userService.GetAll());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            UserDTO? userDto = await _userService.GetById(id);

            if (userDto == null)
                return NotFound("User not found.");

            return Ok(userDto);
        }

        [HttpGet]
        [Route("getByLogin/{login}")]
        public async Task<IActionResult> GetByLogin(string login)
        {
            UserDTO? userDto = await _userService.GetUserByLogin(login);

            if(userDto == null)
                return NotFound("User not found.");

            return Ok(userDto);
        }

        [HttpGet]
        [Route("authenticate/{login}/{password}")]
        public async Task<IActionResult> Authenticate(string login, string password)
        {
            UserDTO? userDTO = await _userService.GetUserByLogin(login);
            string? storedHashedPassword = userDTO.Password;
            bool isPasswordValid = HashFunc.VerifyPassword(password, storedHashedPassword);

            if (isPasswordValid) 
                return Ok("Authenticated successfully.");

            return Unauthorized("Invalid credentials.");
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] UserDTO userDTO)
        {
            await _userService.Create(userDTO);
            return Created("create", userDTO);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserDTO userDTO)
        {
            UserDTO? currentUser = await _userService.GetById(id);

            if (currentUser != null)
            {
                await _userService.Update(id, userDTO);
                return Ok(currentUser);
            } 
           
            return NotFound("User Not Found!");
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            UserDTO? currentUser = await _userService.GetById(id);

            if (currentUser == null)
                return NotFound("User Not Found.");

            await _userService.DeleteById(id);
            return NoContent();
        }
    }
}
