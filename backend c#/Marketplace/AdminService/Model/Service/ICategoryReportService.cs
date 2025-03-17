using AdminService.View.DTO;

namespace AdminService.Model.Service
{
    public interface ICategoryReportService
    {
        Task<List<CategoryReportDTO>> GetAll();

        Task<CategoryReportDTO?> GetById(int id);

        Task<CategoryReportDTO?> GetByName(string name);

        Task Create(CategoryReportDTO categoryReportDTO);

        Task Update(int id, CategoryReportDTO categoryReportDTO);

        Task DeleteById(int id);
    }
}
