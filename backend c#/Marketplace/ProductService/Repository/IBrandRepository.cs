using ProductService.Entity;

namespace ProductService.Repository
{
    public interface IBrandRepository
    {
        Task<List<Brand>> GetAll();

        Task<Brand?> GetById(int id);

        Task Create(Brand brand);

        Task Update(int id, Brand brand);

        Task DeleteById(int id);
    }
}
