using AdminService.Model.Db;
using AdminService.Model.Entity;
using Microsoft.EntityFrameworkCore;

namespace AdminService.Model.Repositroy
{
    public class CategoryReportRepositoryImpl : ICategoryReportRepository
    {
        private readonly AdminContext _adminContext;
        
        public CategoryReportRepositoryImpl(AdminContext adminContext) => _adminContext = adminContext;

        public async Task Create(CategoryReport categoryReport)
        {
            await _adminContext.CategoryReports.AddAsync(categoryReport);
            await _adminContext.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            CategoryReport? categoryReport = await GetById(id);

            if(categoryReport != null)
            {
                _adminContext.CategoryReports.Remove(categoryReport);
                await _adminContext.SaveChangesAsync();
            }
        }

        public async Task<List<CategoryReport>> GetAll()
        {
            return await _adminContext.CategoryReports.ToListAsync();
        }

        public async Task<CategoryReport?> GetById(int id)
        {
            CategoryReport? categoryReport = await _adminContext.CategoryReports.FindAsync(id);

            if(categoryReport != null)
                return categoryReport;

            return null;
        }

        public Task<CategoryReport?> GetByName(string name)
        {
            return _adminContext.CategoryReports
                        .Where(cr => cr.Name.Equals(name))
                        .FirstOrDefaultAsync();
        }

        public async Task Update(int id, CategoryReport categoryReport)
        {
            CategoryReport? currentCategoryReport = await GetById(id);

            if (currentCategoryReport != null)
            {
                currentCategoryReport.Name = categoryReport.Name;
                _adminContext.CategoryReports.Update(currentCategoryReport);
                await _adminContext.SaveChangesAsync();
            }
        }
    }
}
