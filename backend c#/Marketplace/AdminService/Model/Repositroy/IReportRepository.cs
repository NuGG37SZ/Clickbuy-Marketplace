using AdminService.Model.Entity;

namespace AdminService.Model.Repositroy
{
    public interface IReportRepository
    {
        Task<List<Report>> GetAll();

        Task<List<Report>> GetByCategoryReport(int categoryReportId);

        Task<List<Report>> GetByStatus(string status);

        Task<List<Report>> GetByUserId(int userId);

        Task<List<Report>> GetByCategoryReportAndStatusAndUserId(int categoryReportId, int userId, string status);

        Task<Report?> GetById(int id);

        Task Create(Report report); 

        Task Update(int id, Report report);

        Task DeleteById(int id);
    }
}
