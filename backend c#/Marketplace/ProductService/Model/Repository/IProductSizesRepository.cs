﻿using ProductService.Model.Entity;

namespace ProductService.Model.Repository
{
    public interface IProductSizesRepository
    {
        Task<List<ProductSizes>> GetAll();

        Task<List<ProductSizes>> GetAllByProductId(int productId);

        Task<ProductSizes?> GetById(int id);

        Task<ProductSizes?> GetByProductIdAndSize(int productId, string size);

        Task Create(ProductSizes productSizes);

        Task DeleteById(int id);

        Task Update(int productId, List<ProductSizes> newProductsSizes);

        Task Update(int id, ProductSizes productSizes);
    }
}
