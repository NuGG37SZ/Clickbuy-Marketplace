using Microsoft.EntityFrameworkCore;
using ProductService.Db;
using ProductService.Entity;

namespace ProductService.Repository
{
    public class BrandRepositoryImpl : IBrandRepository
    {
        private readonly ProductContext _productContext;

        public BrandRepositoryImpl(ProductContext productContext) => _productContext = productContext;

        public async Task Create(Brand brand)
        {
            await _productContext.Brands.AddAsync(brand);
            await _productContext.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            Brand? currentBrand = await _productContext.Brands.FindAsync(id);

            if (currentBrand != null)
            {
                _productContext.Brands.Remove(currentBrand);
                await _productContext.SaveChangesAsync();
            }
        }

        public async Task<List<Brand>> GetAll()
        {
            return await _productContext.Brands.ToListAsync();
        }

        public async Task<Brand?> GetById(int id)
        {
            Brand? currentBrand = await _productContext.Brands.FindAsync(id);

            if(currentBrand != null)
                return currentBrand;

            return null;
        }

        public async Task Update(int id, Brand brand)
        {
            Brand? currentBrand = await GetById(id);

            if (currentBrand != null)
            {
                currentBrand.Name = brand.Name;
                await _productContext.SaveChangesAsync();
            }
        }
    }
}
