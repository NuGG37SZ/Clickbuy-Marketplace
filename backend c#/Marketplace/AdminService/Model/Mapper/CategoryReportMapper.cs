using AdminService.Model.Entity;
using AdminService.View;
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

        public static CategoryReport MapCategoryReportDTOToCategoryReport(
            CategoryReportDTO categoryReportDTO)
        {
            CategoryReport categoryReport = new CategoryReport();
            categoryReport.Name = categoryReportDTO.Name;
            return categoryReport;
        }

        public static CategoryReportView MapCategoryReportDTOToCategoryReportView(
            CategoryReportDTO categoryReportDTO)
        {
            CategoryReportView categoryReportView = new CategoryReportView();
            categoryReportView.Id = categoryReportDTO.Id;
            categoryReportView.Name = categoryReportDTO.Name;
            return categoryReportView;
        }

        public static List<CategoryReportView> MapCategoryReportDTOListToCategoryReportViewList(
            List<CategoryReportDTO> categoryReportDTOList)
        {
            return categoryReportDTOList.Select(MapCategoryReportDTOToCategoryReportView).ToList();
        }
    }
}
