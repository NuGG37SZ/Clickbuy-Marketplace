using Microsoft.AspNetCore.Mvc;
using OrderService.DTO;
using OrderService.Service;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/v1/userPoints")]
    public class UserPointsController : Controller
    {
        private readonly IUserPointsService _userPointsService;

        public UserPointsController(IUserPointsService userPointsService) => _userPointsService = userPointsService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _userPointsService.GetAll());
        }

        [HttpGet]
        [Route("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            UserPointsDTO? userPointsDTO = await _userPointsService.GetById(id);

            if (userPointsDTO == null)
                return NotFound("UserPoints Not Found.");

            return Ok(userPointsDTO);
        }

        [HttpGet]
        [Route("getByUserId/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            return Ok(await _userPointsService.GetByUserId(userId));
        }

        [HttpGet]
        [Route("getByUserIdAndPointsId/{userId}/{pointId}")]
        public async Task<IActionResult> GetByUserIdAndPointsId(int userId, int pointId)
        {
            UserPointsDTO? userPointsDTO = await _userPointsService.GetByUserIdAndPointsId(userId, pointId);

            if(userPointsDTO != null)
                return Ok(userPointsDTO);

            return Ok(new UserPointsDTO());
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] UserPointsDTO userPointsDTO)
        {
            await _userPointsService.Create(userPointsDTO);
            return Created("create", userPointsDTO);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserPointsDTO userPointsDTO)
        {
            UserPointsDTO? currentUserPoints = await _userPointsService.GetById(id);

            if (currentUserPoints == null)
                return NotFound("UserPoints Not Found.");

            return Ok(userPointsDTO);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            UserPointsDTO? currentUserPoints = await _userPointsService.GetById(id);

            if (currentUserPoints == null)
                return NotFound("UserPoints Not Found.");

            await _userPointsService.DeleteById(id);
            return NoContent();
        } 
    }
}
