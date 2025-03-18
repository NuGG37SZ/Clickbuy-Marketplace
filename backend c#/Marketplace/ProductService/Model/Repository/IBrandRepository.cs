using ProductService.Model.Entity;

namespace ProductService.Model.Repository
{
    public interface IBrandRepository
    {
        Task<List<Brands>> GetAll();

        Task<Brands?> GetById(int id);

        Task Create(Brands brand);

        Task Update(int id, Brands brand);

        Task DeleteById(int id);
    }
}
