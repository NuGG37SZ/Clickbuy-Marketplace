using AdminService.Model.Entity;

namespace AdminService.Model.Repositroy
{
    public interface ICategoryReportRepository
    {
        Task<List<CategoryReport>> GetAll();

        Task<CategoryReport?> GetById(int id);

        Task<CategoryReport?> GetByName(string name);

        Task Create(CategoryReport categoryReport);

        Task Update(int id, CategoryReport categoryReport);

        Task DeleteById(int id);
    }
}
