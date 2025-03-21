using Microsoft.AspNetCore.Mvc;
using OrderService.Model.DTO;
using OrderService.Model.Mapper;
using OrderService.Model.Service;
using OrderService.View;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/v1/userPoints")]
    public class UserPointsController : Controller
    {
        private readonly IUserPointsService _userPointsService;

        public UserPointsController(IUserPointsService userPointsService) => 
            _userPointsService = userPointsService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(UserPointsMapper.MapUserPointDTOListToUserPointViewList(
                await _userPointsService.GetAll()
            ));
        }

        [HttpGet]
        [Route("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            UserPointsDTO? userPointsDTO = await _userPointsService.GetById(id);

            if (userPointsDTO == null)
                return NotFound("UserPoints Not Found.");

            return Ok(UserPointsMapper.MapUserPointDTOToUserPointsView(userPointsDTO));
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
                return Ok(UserPointsMapper.MapUserPointDTOToUserPointsView(userPointsDTO));

            return Ok(new UserPointsView());
        }

        [HttpGet]
        [Route("getByIsActiveAndUserId/{isActive}/{userId}")]
        public async Task<IActionResult> GetByIsActiveAndUserId(bool isActive, int userId)
        {
            UserPointsDTO? userPointsDTO = await _userPointsService.GetByIsActiveAndUserId(isActive, userId);

            if(userPointsDTO != null) 
                return Ok(UserPointsMapper.MapUserPointDTOToUserPointsView(userPointsDTO));

            return Ok(new UserPointsView());
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] UserPointsDTO userPointsDTO)
        {
            await _userPointsService.Create(userPointsDTO);
            return Created("create", UserPointsMapper.MapUserPointDTOToUserPointsView(userPointsDTO));
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserPointsDTO userPointsDTO)
        {
            UserPointsDTO? currentUserPoints = await _userPointsService.GetById(id);

            if (currentUserPoints == null)
                return NotFound("UserPoints Not Found.");

            await _userPointsService.Update(id, userPointsDTO);
            return Ok(UserPointsMapper.MapUserPointDTOToUserPointsView(userPointsDTO));
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
