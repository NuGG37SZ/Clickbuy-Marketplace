using AdminService.Model.Db;
using AdminService.Model.Entity;
using Microsoft.EntityFrameworkCore;

namespace AdminService.Model.Repositroy
{
    public class ReportRepositoryImpl : IReportRepository
    {
        private readonly AdminContext _adminContext;

        public ReportRepositoryImpl(AdminContext adminContext) => _adminContext = adminContext;

        public async Task Create(Report report)
        {
            await _adminContext.Reports.AddAsync(report);
            await _adminContext.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            Report? report = await GetById(id);

            if (report != null)
            {
                _adminContext.Reports.Remove(report);
                await _adminContext.SaveChangesAsync();
            }
        }

        public async Task<List<Report>> GetAll()
        {
            return await _adminContext.Reports.ToListAsync();
        }

        public async Task<List<Report>> GetByCategoryReport(int categoryReportId)
        {
            return await _adminContext.Reports
                            .Where(r => r.CategoryReportId == categoryReportId)
                            .ToListAsync();
        }

        public async Task<List<Report>> GetByCategoryReportAndStatusAndUserId(int categoryReportId, 
            int userId, string status)
        {
            return await _adminContext.Reports
                            .Where(r => r.CategoryReportId == categoryReportId &&
                                    r.UserId == userId &&
                                    r.Status.Equals(status))
                            .ToListAsync();
        }

        public async Task<Report?> GetById(int id)
        {
            Report? report = await _adminContext.Reports.FindAsync(id);

            if (report != null)
                return report;

            return null;
        }

        public async Task<List<Report>> GetByStatus(string status)
        {
            return await _adminContext.Reports
                            .Where(r => r.Status.Equals(status))
                            .ToListAsync();
        }

        public async Task<List<Report>> GetByUserId(int userId)
        {
            return await _adminContext.Reports
                            .Where(r => r.UserId == userId)
                            .ToListAsync();
        }

        public async Task Update(int id, Report report)
        {
            Report? currentReport = await GetById(id);

            if(currentReport != null)
            {
                currentReport.UserId = report.UserId;
                currentReport.CategoryReportId = report.CategoryReportId;
                currentReport.Subject = report.Subject;
                currentReport.Status = report.Status;
                currentReport.Description = report.Description;
                _adminContext.Reports.Update(currentReport);
                await _adminContext.SaveChangesAsync();
            }
        }
    }
}
