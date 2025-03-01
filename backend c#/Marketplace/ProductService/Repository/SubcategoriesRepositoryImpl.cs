using Microsoft.EntityFrameworkCore;
using ProductService.Db;
using ProductService.Entity;

namespace ProductService.Repository
{
    public class SubcategoriesRepositoryImpl : ISubcategoriesRepository
    {
        private readonly ProductContext _productContext;

        public SubcategoriesRepositoryImpl(ProductContext productContext) =>
            _productContext = productContext;

        public async Task Create(Subcategories subcategory)
        {
            await _productContext.Subcategories.AddAsync(subcategory);
            await _productContext.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            Subcategories? currentSubcategories = await GetById(id);

            if (currentSubcategories != null)
            {
                _productContext.Remove(currentSubcategories);
                await _productContext.SaveChangesAsync();
            }
        }

        public async Task<List<Subcategories>> GetAll()
        {
            return await _productContext.Subcategories.ToListAsync();
        }

        public async Task<Subcategories?> GetById(int id)
        {
            Subcategories? currentSubcategories =
                await _productContext.Subcategories.FindAsync(id);

            if (currentSubcategories != null)
                return currentSubcategories;

            return null;
        }

        public async Task Update(int id, Subcategories subcategory)
        {
            Subcategories? currentSubcategories = await GetById(id);

            if (currentSubcategories != null)
            {
                currentSubcategories.Name = subcategory.Name;
                currentSubcategories.CategoryId = subcategory.CategoryId;
                _productContext.Subcategories.Update(currentSubcategories);
                await _productContext.SaveChangesAsync();
            }
        }
    }
}
