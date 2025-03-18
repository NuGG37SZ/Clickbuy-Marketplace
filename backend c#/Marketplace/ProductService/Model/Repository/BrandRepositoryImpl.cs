using Microsoft.EntityFrameworkCore;
using ProductService.Model.Db;
using ProductService.Model.Entity;

namespace ProductService.Model.Repository
{
    public class BrandRepositoryImpl : IBrandRepository
    {
        private readonly ProductContext _productContext;

        public BrandRepositoryImpl(ProductContext productContext) => _productContext = productContext;

        public async Task Create(Brands brand)
        {
            await _productContext.Brands.AddAsync(brand);
            await _productContext.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            Brands? currentBrand = await _productContext.Brands.FindAsync(id);

            if (currentBrand != null)
            {
                _productContext.Brands.Remove(currentBrand);
                await _productContext.SaveChangesAsync();
            }
        }

        public async Task<List<Brands>> GetAll()
        {
            return await _productContext.Brands.ToListAsync();
        }

        public async Task<Brands?> GetById(int id)
        {
            Brands? currentBrand = await _productContext.Brands.FindAsync(id);

            if (currentBrand != null)
                return currentBrand;

            return null;
        }

        public async Task Update(int id, Brands brand)
        {
            Brands? currentBrand = await GetById(id);

            if (currentBrand != null)
            {
                currentBrand.Name = brand.Name;
                await _productContext.SaveChangesAsync();
            }
        }
    }
}
