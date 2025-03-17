using AdminService.Model.Entity;
using AdminService.Model.Mapper;
using AdminService.Model.Repositroy;
using AdminService.View.DTO;

namespace AdminService.Model.Service
{
    public class ReportServiceImpl : IReportService
    {
        private readonly IReportRepository _reportRepository;

        public ReportServiceImpl(IReportRepository reportRepository) => _reportRepository = reportRepository;

        public async Task Create(ReportDTO reportDTO)
        {
            await _reportRepository.Create(ReportMapper.MapReportDTOToReport(reportDTO));
        }

        public async Task DeleteById(int id)
        {
            ReportDTO? reportDTO = await GetById(id);

            if(reportDTO != null) 
                await _reportRepository.DeleteById(id);
        }

        public async Task<List<ReportDTO>> GetAll()
        {
            List<Report> reports = await _reportRepository.GetAll();

            return reports
                    .Select(ReportMapper.MapReportToReportDTO)
                    .ToList();
        }

        public async Task<List<ReportDTO>> GetByCategoryReport(int categoryReportId)
        {
            List<Report> reports = await _reportRepository.GetByCategoryReport(categoryReportId);

            return reports
                    .Select(ReportMapper.MapReportToReportDTO)
                    .ToList();
        }

        public async Task<List<ReportDTO>> GetByCategoryReportAndStatusAndUserId(int categoryReportId, 
            int userId, string status)
        {
            List<Report> reports = await _reportRepository.GetByCategoryReportAndStatusAndUserId(
                categoryReportId, userId, status);

            return reports
                    .Select(ReportMapper.MapReportToReportDTO)
                    .ToList();
        }

        public async Task<ReportDTO?> GetById(int id)
        {
            Report? report = await _reportRepository.GetById(id);

            if(report != null)
                return ReportMapper.MapReportToReportDTO(report);

            return null;
        }

        public async Task<List<ReportDTO>> GetByStatus(string status)
        {
            List<Report> reports = await _reportRepository.GetByStatus(status);

            return reports
                    .Select(ReportMapper.MapReportToReportDTO)
                    .ToList();
        }

        public async Task<List<ReportDTO>> GetByUserId(int userId)
        {
            List<Report> reports = await _reportRepository.GetByUserId(userId);

            return reports
                    .Select(ReportMapper.MapReportToReportDTO)
                    .ToList();
        }

        public async Task Update(int id, ReportDTO reportDTO)
        {
            ReportDTO? currentReportDTO = await GetById(id);

            if(currentReportDTO != null) 
                await _reportRepository.Update(id, ReportMapper.MapReportDTOToReport(reportDTO));
        }
    }
}
