using AdminService.Model.Entity;
using AdminService.View;
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

        public static ReportView MapReportDTOToReportView(ReportDTO reportDTO)
        {
            ReportView reportView = new ReportView();
            reportView.Id = reportDTO.Id;
            reportView.UserId = reportDTO.UserId;
            reportView.CategoryReportId = reportDTO.CategoryReportId;
            reportView.Subject = reportDTO.Subject;
            reportView.Status = reportDTO.Status;
            reportView.Description = reportDTO.Description;
            return reportView;
        }

        public static List<ReportView> MapReportDTOListToReportViewList(List<ReportDTO> reportDTO)
        {
            return reportDTO.Select(MapReportDTOToReportView).ToList();
        }
    }
}
