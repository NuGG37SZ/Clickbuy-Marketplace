﻿using Microsoft.EntityFrameworkCore;
using RatingService.Model.Db;
using RatingService.Model.Entity;

namespace RatingService.Model.Repository
{
    public class RatingProductRepositoryImpl : IRatingProductRepository
    {
        private readonly RatingContext _ratingContext;

        public RatingProductRepositoryImpl(RatingContext ratingContext) => _ratingContext = ratingContext;

        public async Task Create(RatingProduct ratingProduct)
        {
            await _ratingContext.RatingProducts.AddAsync(ratingProduct);
            await _ratingContext.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            RatingProduct? ratingProduct = await GetById(id);

            if (ratingProduct != null)
            {
                _ratingContext.RatingProducts.Remove(ratingProduct);
                await _ratingContext.SaveChangesAsync();
            }
        }

        public async Task<List<RatingProduct>> GetAll()
        {
            return await _ratingContext.RatingProducts.ToListAsync();
        }

        public async Task<RatingProduct?> GetById(int id)
        {
            RatingProduct? ratingProduct = await _ratingContext.RatingProducts.FindAsync(id);

            if (ratingProduct != null)
                return ratingProduct;

            return null;
        }

        public async Task<List<RatingProduct>> GetByProductId(int productId)
        {
            return await _ratingContext.RatingProducts
                                    .Where(rp => rp.ProductId == productId)
                                    .ToListAsync();
        }

        public async Task<RatingProduct?> GetByProductIdAndProductSizesIdAndOrderId(int productId,
            int productSizesId, int orderId)
        {
            return await _ratingContext.RatingProducts
                                   .Where(rp => rp.ProductId == productId &&
                                        rp.ProductSizesId == productSizesId &&
                                        rp.OrderId == orderId)
                                   .FirstOrDefaultAsync();
        }

        public async Task<List<RatingProduct>> GetByUserId(int userId)
        {
            return await _ratingContext.RatingProducts
                                    .Where(rp => rp.UserId == userId)
                                    .ToListAsync();
        }

        public async Task<double> AvgRatingByProductId(int productId)
        {
            return await _ratingContext.RatingProducts
                            .Where(rp => rp.ProductId == productId && rp.Comment != "" && rp.Rating != 0.0)
                            .AverageAsync(rp => rp.Rating);
        }

        public async Task Update(int id, RatingProduct ratingProduct)
        {
            RatingProduct? currentRatingProduct = await GetById(id);

            if (currentRatingProduct != null)
            {
                currentRatingProduct.ProductId = ratingProduct.ProductId;
                currentRatingProduct.ProductSizesId = ratingProduct.ProductSizesId;
                currentRatingProduct.UserId = ratingProduct.UserId;
                currentRatingProduct.Rating = ratingProduct.Rating;
                currentRatingProduct.Comment = ratingProduct.Comment;
                currentRatingProduct.OrderId = ratingProduct.OrderId;
                currentRatingProduct.DateCreateComment = ratingProduct.DateCreateComment;
                _ratingContext.RatingProducts.Update(currentRatingProduct);
                await _ratingContext.SaveChangesAsync();
            }
        }

        public async Task<double> CountRatingByUserIdAndEmptyComment(int userId)
        {
            return await _ratingContext.RatingProducts
                            .Where(rp => rp.UserId == userId && rp.Comment == "")
                            .CountAsync();
        }

        public async Task<double> CountEmptyCommentByProductId(int productId)
        {
            return await _ratingContext.RatingProducts
                            .Where(rp => rp.ProductId == productId && rp.Comment == "")
                            .CountAsync();
        }

        public async Task<List<RatingProduct>> GetByUserIdAndProductId(int userId, int productId)
        {
            return await _ratingContext.RatingProducts
                            .Where(rp => rp.UserId == userId &&
                                    rp.ProductId == productId)
                            .ToListAsync();
        }
    }
}
