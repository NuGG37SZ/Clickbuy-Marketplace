using AdminService.Model.Entity;
using AdminService.Model.Mapper;
using AdminService.Model.Repositroy;
using AdminService.View.DTO;

namespace AdminService.Model.Service
{
    public class CategoryReportServiceImpl : ICategoryReportService
    {
        private readonly ICategoryReportRepository _categoryReportRepository;
        
        public CategoryReportServiceImpl(ICategoryReportRepository categoryReportRepository) =>
            _categoryReportRepository = categoryReportRepository;

        public async Task Create(CategoryReportDTO categoryReportDTO)
        {
            await _categoryReportRepository.Create(
                CategoryReportMapper.MapCategoryReportDTOToCategoryReport(categoryReportDTO)
            );
        }

        public async Task DeleteById(int id)
        {
            CategoryReportDTO? categoryReportDTO = await GetById(id);

            if (categoryReportDTO != null)
                await _categoryReportRepository.DeleteById(id);
        }

        public async Task<List<CategoryReportDTO>> GetAll()
        {
            List<CategoryReport> categories = await _categoryReportRepository.GetAll();

            return categories
                    .Select(CategoryReportMapper.MapCategoryReportToCategoryReportDTO)
                    .ToList();
        }

        public async Task<CategoryReportDTO?> GetById(int id)
        {
            CategoryReport? categoryReport = await _categoryReportRepository.GetById(id);

            if(categoryReport != null)
                return CategoryReportMapper.MapCategoryReportToCategoryReportDTO(categoryReport);

            return null;
        }

        public async Task<CategoryReportDTO?> GetByName(string name)
        {
            CategoryReport? categoryReport = await _categoryReportRepository.GetByName(name);

            if (categoryReport != null)
                return CategoryReportMapper.MapCategoryReportToCategoryReportDTO(categoryReport);

            return null;
        }

        public async Task Update(int id, CategoryReportDTO categoryReportDTO)
        {
            CategoryReportDTO? currentCategoryReportDTO = await GetById(id);

            if (categoryReportDTO != null)
            {
                await _categoryReportRepository.Update(id, 
                    CategoryReportMapper.MapCategoryReportDTOToCategoryReport(categoryReportDTO));
            }
        }
    }
}
