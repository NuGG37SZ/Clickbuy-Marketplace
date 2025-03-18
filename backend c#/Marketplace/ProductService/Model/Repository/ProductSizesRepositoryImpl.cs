using Microsoft.EntityFrameworkCore;
using ProductService.Model.Db;
using ProductService.Model.Entity;

namespace ProductService.Model.Repository
{
    public class ProductSizesRepositoryImpl : IProductSizesRepository
    {
        private readonly ProductContext _productContext;

        public ProductSizesRepositoryImpl(ProductContext productContext) => _productContext = productContext;

        public async Task Create(ProductSizes productSizes)
        {
            await _productContext.ProductSizes.AddAsync(productSizes);
            await _productContext.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            ProductSizes? productSizes = await GetById(id);

            if (productSizes != null)
            {
                _productContext.ProductSizes.Remove(productSizes);
                await _productContext.SaveChangesAsync();
            }
        }

        public async Task<List<ProductSizes>> GetAll()
        {
            return await _productContext.ProductSizes.ToListAsync();
        }

        public async Task<List<ProductSizes>> GetAllByProductId(int productId)
        {
            return await _productContext.ProductSizes
                            .Where(ps => ps.ProductId == productId)
                            .ToListAsync();
        }

        public async Task<ProductSizes?> GetById(int id)
        {
            return await _productContext.ProductSizes.FindAsync(id);
        }

        public async Task<ProductSizes?> GetByProductIdAndSize(int productId, string size)
        {
            return await _productContext.ProductSizes
                            .Where(p => p.ProductId == productId && p.Size.Equals(size))
                            .FirstOrDefaultAsync();
        }

        public async Task Update(int productId, List<ProductSizes> newProductsSizes)
        {
            List<ProductSizes> productSizesList = await GetAllByProductId(productId);

            for (int i = 0; i < productSizesList.Count; i++)
            {
                productSizesList[i].Size = newProductsSizes[i].Size;
                productSizesList[i].ProductId = newProductsSizes[i].ProductId;
                productSizesList[i].Count = newProductsSizes[i].Count;
            }

            _productContext.ProductSizes.UpdateRange(productSizesList);
            await _productContext.SaveChangesAsync();
        }

        public async Task Update(int id, ProductSizes productSizes)
        {
            ProductSizes? currentProductSizes = await GetById(id);

            if (currentProductSizes != null)
            {
                currentProductSizes.ProductId = productSizes.ProductId;
                currentProductSizes.Size = productSizes.Size;
                currentProductSizes.Count = productSizes.Count;
                _productContext.Update(currentProductSizes);
                await _productContext.SaveChangesAsync();
            }
        }
    }
}
