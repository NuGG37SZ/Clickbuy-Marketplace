﻿using ProductService.Db;
using ProductService.Entity;

namespace ProductService.Repository
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
            Product currentProduct = await GetById(id);

            if (currentProduct != null)
            {
                _productContext.Products.Remove(currentProduct);
                await _productContext.SaveChangesAsync();
            }
        }

        public List<Product> GetAll()
        {
            return _productContext.Products.ToList();
        }

        public async Task<Product> GetById(int id)
        {
            Product currentProduct = await _productContext.Products.FindAsync(id);

            if (currentProduct != null)
                return currentProduct;

            return null;
        }

        public async Task Update(int id, Product product)
        {
            Product currentProduct = await GetById(id);

            if (currentProduct != null)
            {
                currentProduct.UserId = product.UserId;
                currentProduct.Name = product.Name;
                currentProduct.Description = product.Description;
                currentProduct.Price = product.Price;
                currentProduct.Count = product.Count;
                _productContext.Update(currentProduct);
                await _productContext.SaveChangesAsync();
            }
        }
    }
}
