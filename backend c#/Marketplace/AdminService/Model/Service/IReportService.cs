using AdminService.View.DTO;

namespace AdminService.Model.Service
{
    public interface IReportService
    {
        Task<List<ReportDTO>> GetAll();

        Task<List<ReportDTO>> GetByCategoryReport(int categoryReportId);

        Task<List<ReportDTO>> GetByStatus(string status);

        Task<List<ReportDTO>> GetByUserId(int userId);

        Task<List<ReportDTO>> GetByCategoryReportAndStatusAndUserId(int categoryReportId, int userId, string status);

        Task<ReportDTO?> GetById(int id);

        Task Create(ReportDTO reportDTO);

        Task Update(int id, ReportDTO reportDTO);

        Task DeleteById(int id);
    }
}
