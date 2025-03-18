using Microsoft.EntityFrameworkCore;
using ProductService.Model.Db;
using ProductService.Model.Entity;

namespace ProductService.Model.Repository
{
    public class ProductRepositoryImpl : IProductRepository
    {
        private readonly ProductContext _productContext;

        public ProductRepositoryImpl(ProductContext productContext)
        {
            _productContext = productContext;
        }

        public async Task Create(Product product)
        {
            await _productContext.Products.AddAsync(product);
            await _productContext.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            Product? currentProduct = await GetById(id);

            if (currentProduct != null)
            {
                _productContext.Products.Remove(currentProduct);
                await _productContext.SaveChangesAsync();
            }
        }

        public async Task DeleteByProductNameAndUserId(string name, int userId)
        {
            Product? currentProduct = await _productContext.Products
                                        .Where(p => p.Name == name && p.UserId == userId)
                                        .FirstOrDefaultAsync();

            if (currentProduct != null)
            {
                _productContext.Remove(currentProduct);
                await _productContext.SaveChangesAsync();
            }
        }

        public async Task<List<Product>> GetAll()
        {
            return await _productContext.Products.ToListAsync();
        }

        public async Task<List<Product>> GetByBrandSubcategoryId(int brandSubcategoryId)
        {
            return await _productContext.Products
                               .Where(p => p.BrandsSubcategoriesId == brandSubcategoryId)
                               .ToListAsync();
        }

        public async Task<Product?> GetById(int id)
        {
            Product? currentProduct = await _productContext.Products.FindAsync(id);

            if (currentProduct != null)
                return currentProduct;

            return null;
        }

        public async Task<List<Product>> GetByNameAndUserId(string name, int userId)
        {
            return await _productContext.Products
                            .Where(p => p.UserId == userId &&
                            EF.Functions.Like(p.Name, $"%{name}%"))
                            .ToListAsync();

        }

        public async Task<Product?> GetByProductNameAndUserId(string name, int userId)
        {
            return await _productContext.Products
                            .Where(p => p.Name == name && p.UserId == userId)
                            .FirstOrDefaultAsync();
        }

        public Task<List<Product>> GetByUserId(int userId)
        {
            return _productContext.Products
                        .Where(p => p.UserId == userId)
                        .ToListAsync();
        }

        public async Task Update(int id, Product product)
        {
            Product? currentProduct = await GetById(id);

            if (currentProduct != null)
            {
                currentProduct.BrandsSubcategoriesId = product.BrandsSubcategoriesId;
                currentProduct.UserId = product.UserId;
                currentProduct.Name = product.Name;
                currentProduct.Description = product.Description;
                currentProduct.Price = product.Price;
                currentProduct.ImageUrl = product.ImageUrl;
                _productContext.Update(currentProduct);
                await _productContext.SaveChangesAsync();
            }
        }
    }
}
