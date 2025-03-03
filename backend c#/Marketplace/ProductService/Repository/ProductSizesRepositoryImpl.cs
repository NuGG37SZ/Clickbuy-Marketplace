﻿using Microsoft.EntityFrameworkCore;
using ProductService.Db;
using ProductService.Entity;

namespace ProductService.Repository
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
            return await _productContext.ProductSizes
                            .FirstOrDefaultAsync(ps => ps.ProductId == id);
        }
    }
}
