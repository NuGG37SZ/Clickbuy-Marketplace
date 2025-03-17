using AdminService.Model.Entity;
using AdminService.View.DTO;

namespace AdminService.Model.Mapper
{
    public class ReportMapper
    {
        public static ReportDTO MapReportToReportDTO(Report report)
        {
            ReportDTO reportDTO = new ReportDTO();
            reportDTO.Id = report.Id;
            reportDTO.UserId = report.UserId;
            reportDTO.CategoryReportId = report.CategoryReportId;
            reportDTO.Subject = report.Subject;
            reportDTO.Status = report.Status;
            reportDTO.Description = report.Description;
            return reportDTO;
        }

        public static Report MapReportDTOToReport(ReportDTO reportDTO)
        {
            Report report = new Report();
            report.UserId = reportDTO.UserId;
            report.CategoryReportId = reportDTO.CategoryReportId;
            report.Subject = reportDTO.Subject;
            report.Status = reportDTO.Status;
            report.Description = reportDTO.Description;
            return report;
        }
    }
}
