using AdminService.Model.Mapper;
using AdminService.Model.Service;
using AdminService.View.DTO;
using Microsoft.AspNetCore.Mvc;

namespace AdminService.Controllers
{
    [ApiController]
    [Route("api/v1/report")]
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService) => _reportService = reportService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(ReportMapper.MapReportDTOListToReportViewList(await _reportService.GetAll()));
        }

        [HttpGet]
        [Route("getByCategoryReportId/{categoryReportId}")]
        public async Task<IActionResult> GetByCategoryReportId(int categoryReportId)
        {
            return Ok(ReportMapper.MapReportDTOListToReportViewList(
                await _reportService.GetByCategoryReport(categoryReportId)
            ));
        }


        [HttpGet]
        [Route("getByStatus/{status}")]
        public async Task<IActionResult> GetByStatus(string status)
        {
            return Ok(ReportMapper.MapReportDTOListToReportViewList(
                await _reportService.GetByStatus(status)
            ));
        }


        [HttpGet]
        [Route("getByUserId/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            return Ok(ReportMapper.MapReportDTOListToReportViewList(
                await _reportService.GetByUserId(userId)
            ));
        }

        [HttpGet]
        [Route("getByCategoryReportAndStatusAndUserId/{categoryReportId}/{userId}/{status}")]
        public async Task<IActionResult> GetByCategoryReportAndStatusAndUserId(int categoryReportId,
            int userId, string status)
        {
            return Ok(ReportMapper.MapReportDTOListToReportViewList(
                await _reportService.GetByCategoryReportAndStatusAndUserId(
                    categoryReportId, userId, status)
            ));
        }

        [HttpGet]
        [Route("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            ReportDTO? reportDTO = await _reportService.GetById(id);

            if (reportDTO == null)
                return NotFound("Report Not Found.");

            return Ok(ReportMapper.MapReportDTOToReportView(reportDTO));
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] ReportDTO reportDTO)
        {
            await _reportService.Create(reportDTO);
            return Created("create", ReportMapper.MapReportDTOToReportView(reportDTO));
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ReportDTO reportDTO)
        {
            ReportDTO? currentReportDTO = await _reportService.GetById(id);

            if (currentReportDTO == null)
                return NotFound("Report Not Found.");

            await _reportService.Update(id, reportDTO);
            return Ok(ReportMapper.MapReportDTOToReportView(reportDTO));
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            ReportDTO? currentReportDTO = await _reportService.GetById(id);

            if (currentReportDTO == null)
                return NotFound("Report Not Found.");

            await _reportService.DeleteById(id);
            return NoContent();
        }
    }
}
