﻿using RatingService.Entity;

namespace RatingService.Repository
{
    public interface IRatingProductRepository 
    {
        Task<List<RatingProduct>> GetAll();

        Task<RatingProduct?> GetById(int id);

        Task<RatingProduct?> GetByProductIdAndProductSizesIdAndOrderId(int productId, int productSizesId, int orderId);

        Task<List<RatingProduct>> GetByProductId(int productId);

        Task<List<RatingProduct>> GetByUserId(int userId);

        Task<double> AvgRatingByProductId(int productId);

        Task<double> CountRatingByUserIdAndEmptyComment(int userId);

        Task Create(RatingProduct ratingProduct);

        Task Update(int id, RatingProduct ratingProduct);

        Task DeleteById(int id);
    }
}
