using AdminService.Model.Mapper;
using AdminService.Model.Service;
using AdminService.View.DTO;
using Microsoft.AspNetCore.Mvc;

namespace AdminService.Controllers
{
    [ApiController]
    [Route("api/v1/categoryReport")]
    public class CategoryReportController : Controller
    {
        private readonly ICategoryReportService _categoryReportService;

        public CategoryReportController(ICategoryReportService categoryReportService) => 
            _categoryReportService = categoryReportService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(CategoryReportMapper.MapCategoryReportDTOListToCategoryReportViewList(
                await _categoryReportService.GetAll())
            );
        }

        [HttpGet]
        [Route("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            CategoryReportDTO? categoryReportDTO = await _categoryReportService.GetById(id);

            if(categoryReportDTO == null)
                return NotFound("CategoryReport Not Found.");

            return Ok(CategoryReportMapper.MapCategoryReportDTOToCategoryReportView(categoryReportDTO)); 
        }

        [HttpGet]
        [Route("getByName/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            CategoryReportDTO? categoryReportDTO = await _categoryReportService.GetByName(name);

            if (categoryReportDTO == null)
                return NotFound("CategoryReport Not Found.");

            return Ok(CategoryReportMapper.MapCategoryReportDTOToCategoryReportView(categoryReportDTO));
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CategoryReportDTO categoryReportDTO)
        {
            await _categoryReportService.Create(categoryReportDTO);
            return Created("create", 
                CategoryReportMapper.MapCategoryReportDTOToCategoryReportView(categoryReportDTO));
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryReportDTO categoryReportDTO)
        {
            CategoryReportDTO? currentCategoryReport = await _categoryReportService.GetById(id);

            if (currentCategoryReport == null)
                return NotFound("CategoryReport Not Found.");

            await _categoryReportService.Update(id, categoryReportDTO);
            return Ok(CategoryReportMapper.MapCategoryReportDTOToCategoryReportView(categoryReportDTO));
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            CategoryReportDTO? currentCategoryReport = await _categoryReportService.GetById(id);

            if(currentCategoryReport == null)
                return NotFound("CategoryReport Not Found.");

            await _categoryReportService.DeleteById(id);
            return NoContent();
        }
    }
}
