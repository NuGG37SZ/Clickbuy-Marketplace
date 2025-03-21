using Microsoft.EntityFrameworkCore;
using ProductService.Model.Db;
using ProductService.Model.Entity;

namespace ProductService.Model.Repository
{
    public class CategoryRepositoryImpl : ICategoryRepository
    {
        private readonly ProductContext _productContext;

        public CategoryRepositoryImpl(ProductContext productContext)
        {
            _productContext = productContext;
        }

        public async Task Create(Category category)
        {
            await _productContext.Categories.AddAsync(category);
            await _productContext.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            Category? currentCategory = await GetById(id);

            if (currentCategory != null)
            {
                _productContext.Categories.Remove(currentCategory);
                await _productContext.SaveChangesAsync();
            }
        }

        public async Task<List<Category>> GetAll()
        {
            return await _productContext.Categories.ToListAsync();
        }

        public async Task<Category?> GetById(int id)
        {
            Category? currentCategory = await _productContext.Categories.FindAsync(id);

            if (currentCategory != null)
                return currentCategory;

            return null;
        }

        public async Task<Category?> GetByName(string name)
        {
            return await _productContext.Categories
                            .Where(c => c.Name.Equals(name))
                            .FirstOrDefaultAsync();
        }

        public async Task Update(int id, Category category)
        {
            Category? currentCategory = await GetById(id);

            if (currentCategory != null)
            {
                currentCategory.Name = category.Name;
                await _productContext.SaveChangesAsync();
            }
        }
    }
}
