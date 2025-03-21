using Microsoft.AspNetCore.Mvc;
using OrderService.Model.DTO;
using OrderService.Model.Mapper;
using OrderService.Model.Service;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/v1/points")]
    public class PointsController : Controller
    {
        private readonly IPointsService _pointsService;

        public PointsController(IPointsService pointsService) => _pointsService = pointsService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(PointsMapper.MapPointsDTOListToPointsViewList(await _pointsService.GetAll()));
        }

        [HttpGet]
        [Route("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            PointsDTO? pointsDTO = await _pointsService.GetById(id);

            if (pointsDTO == null)
                return NotFound("Points Not Found.");

            return Ok(PointsMapper.MapPointsDTOToPointsView(pointsDTO));
        }

        [HttpGet]
        [Route("getByAddress/{address}")]
        public async Task<IActionResult> GetByAddress(string address)
        {
            PointsDTO? pointsDTO = await _pointsService.GetByAddress(address);

            if(pointsDTO == null)
                return Ok(new PointsDTO());

            return Ok(PointsMapper.MapPointsDTOToPointsView(pointsDTO));
        }

        [HttpGet]
        [Route("getByToken/{token}")]
        public async Task<IActionResult> GetByToken(string token)
        {
            PointsDTO? pointsDTO = await _pointsService.GetByToken(token);

            if (pointsDTO == null)
                return NotFound("Points Not Found.");

            return Ok(PointsMapper.MapPointsDTOToPointsView(pointsDTO));
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] PointsDTO pointsDTO)
        {
            await _pointsService.Create(pointsDTO);
            return Created("create", PointsMapper.MapPointsDTOToPointsView(pointsDTO));
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PointsDTO pointsDTO)
        {
            PointsDTO? currentPointsDTO = await _pointsService.GetById(id);

            if (currentPointsDTO == null)
                return NotFound("Points Not Found.");

            await _pointsService.Update(id, pointsDTO);
            return Ok(PointsMapper.MapPointsDTOToPointsView(pointsDTO));
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            PointsDTO? currentPointsDTO = await _pointsService.GetById(id);

            if (currentPointsDTO == null)
                return NotFound("Points Not Found.");

            await _pointsService.DeleteById(id);
            return NoContent();
        }
    }
}
