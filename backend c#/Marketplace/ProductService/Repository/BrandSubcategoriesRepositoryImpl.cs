using Microsoft.EntityFrameworkCore;
using ProductService.Db;
using ProductService.Entity;

namespace ProductService.Repository
{
    public class BrandSubcategoriesRepositoryImpl : IBrandSubcategoriesRepository
    {
        private readonly ProductContext _productContext;

        public BrandSubcategoriesRepositoryImpl(ProductContext productContext) =>
            _productContext = productContext;

        public async Task Create(BrandsSubcategories brandSubcategories)
        {
            await _productContext.BrandSubcategories.AddAsync(brandSubcategories);
            await _productContext.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            BrandsSubcategories? brandSubcategories = await GetById(id);

            if (brandSubcategories != null)
            {
                _productContext.Remove(brandSubcategories);
                await _productContext.SaveChangesAsync();
            }
        }

        public async Task<List<BrandsSubcategories>> GetAll()
        {
            return await _productContext.BrandSubcategories.ToListAsync();
        }

        public async Task<BrandsSubcategories> GetByBrandAndSubcategories(int brandId, int subcategoryId)
        {
            BrandsSubcategories? brandsSubcategories =  await _productContext.BrandSubcategories
                               .Where(bs => bs.BrandsId == brandId)
                               .Where(bs => bs.SubcategoriesId == subcategoryId)
                               .FirstOrDefaultAsync();

            if(brandsSubcategories != null)
                return brandsSubcategories;

            brandsSubcategories = new BrandsSubcategories();
            brandsSubcategories.BrandsId = brandId;
            brandsSubcategories.SubcategoriesId = subcategoryId;
            await _productContext.BrandSubcategories.AddAsync(brandsSubcategories);
            await _productContext.SaveChangesAsync();
            return brandsSubcategories;
        }

        public Task<List<BrandsSubcategories>> GetByBrandId(int id)
        {
           return _productContext.BrandSubcategories
                    .Where(bs => bs.BrandsId == id)
                    .ToListAsync();
        }

        public async Task<BrandsSubcategories?> GetById(int id)
        {
            BrandsSubcategories? brandSubcategories = await _productContext.BrandSubcategories.FindAsync(id);

            if(brandSubcategories != null)
                return brandSubcategories;

            return null;
        }

        public Task<List<BrandsSubcategories>> GetBySubcategoriesId(int id)
        {
            return _productContext.BrandSubcategories
                     .Where(bs => bs.SubcategoriesId == id)
                     .ToListAsync();
        }
    }
}
