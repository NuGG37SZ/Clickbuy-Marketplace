using AdminService.Model.Entity;
using AdminService.View.DTO;

namespace AdminService.Model.Mapper
{
    public class CategoryReportMapper
    {
        public static CategoryReportDTO MapCategoryReportToCategoryReportDTO(CategoryReport categoryReport)
        {
            CategoryReportDTO categoryReportDTO = new CategoryReportDTO();
            categoryReportDTO.Id = categoryReport.Id;
            categoryReportDTO.Name = categoryReport.Name;
            return categoryReportDTO;
        }

        public static CategoryReport MapCategoryReportDTOToCategoryReport(CategoryReportDTO categoryReportDTO)
        {
            CategoryReport categoryReport = new CategoryReport();
            categoryReport.Name = categoryReportDTO.Name;
            return categoryReport;
        }
    }
}
