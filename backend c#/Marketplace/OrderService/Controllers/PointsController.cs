using Microsoft.AspNetCore.Mvc;
using OrderService.DTO;
using OrderService.Service;

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
            return Ok(await _pointsService.GetAll());
        }

        [HttpGet]
        [Route("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            PointsDTO? pointsDTO = await _pointsService.GetById(id);

            if (pointsDTO == null)
                return NotFound("Points Not Found.");

            return Ok(pointsDTO);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] PointsDTO pointsDTO)
        {
            await _pointsService.Create(pointsDTO);
            return Created("create", pointsDTO);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PointsDTO pointsDTO)
        {
            PointsDTO? currentPointsDTO = await _pointsService.GetById(id);

            if (currentPointsDTO == null)
                return NotFound("Points Not Found.");

            await _pointsService.Update(id, pointsDTO);
            return Ok(pointsDTO);
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
