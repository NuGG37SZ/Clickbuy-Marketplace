﻿using RatingService.Model.Entity;
using RatingService.Model.Mapper;
using RatingService.Model.Repository;
using RatingService.View.DTO;

namespace RatingService.Model.Service
{
    public class RatingProductServiceImpl : IRatingProductService
    {
        private readonly IRatingProductRepository _ratingProductRepository;

        public RatingProductServiceImpl(IRatingProductRepository ratingProductRepository) =>
            _ratingProductRepository = ratingProductRepository;

        public async Task Create(RatingProductDTO ratingProductDTO)
        {
            await _ratingProductRepository.Create(
                RatingProductMapper.MapRatingProductDTOToRatingProduct(ratingProductDTO)
            );
        }

        public async Task DeleteById(int id)
        {
            RatingProductDTO? ratingProductDTO = await GetById(id);

            if (ratingProductDTO != null)
                await _ratingProductRepository.DeleteById(id);
        }

        public async Task<List<RatingProductDTO>> GetAll()
        {
            List<RatingProduct> ratingProducts = await _ratingProductRepository.GetAll();

            return ratingProducts
                       .Select(RatingProductMapper.MapRatingProductToRatingProductDTO)
                       .ToList();
        }

        public async Task<RatingProductDTO?> GetById(int id)
        {
            RatingProduct? ratingProduct = await _ratingProductRepository.GetById(id);

            if (ratingProduct != null)
                return RatingProductMapper.MapRatingProductToRatingProductDTO(ratingProduct);

            return null;
        }

        public async Task<List<RatingProductDTO>> GetByProductId(int productId)
        {
            List<RatingProduct> ratingProducts = await _ratingProductRepository.GetByProductId(productId);

            return ratingProducts
                       .Select(RatingProductMapper.MapRatingProductToRatingProductDTO)
                       .ToList();
        }

        public async Task<RatingProductDTO?> GetByProductIdAndProductSizesIdAndOrderId(int productId,
            int productSizesId, int orderId)
        {
            RatingProduct? ratingProduct = await _ratingProductRepository
                .GetByProductIdAndProductSizesIdAndOrderId(productId, productSizesId, orderId);

            if (ratingProduct != null)
                return RatingProductMapper.MapRatingProductToRatingProductDTO(ratingProduct);

            return null;
        }

        public async Task<List<RatingProductDTO>> GetByUserId(int userId)
        {
            List<RatingProduct> ratingProducts = await _ratingProductRepository.GetByUserId(userId);

            return ratingProducts
                       .Select(RatingProductMapper.MapRatingProductToRatingProductDTO)
                       .ToList();
        }

        public async Task<double> AvgRatingByProductId(int productId)
        {
            return await _ratingProductRepository.AvgRatingByProductId(productId);
        }

        public async Task Update(int id, RatingProductDTO ratingProductDTO)
        {
            RatingProductDTO? currentRatingProductDTO = await GetById(id);

            if (currentRatingProductDTO != null)
            {
                await _ratingProductRepository.Update(id,
                    RatingProductMapper.MapRatingProductDTOToRatingProduct(ratingProductDTO));
            }
        }

        public async Task<double> CountRatingByUserIdAndEmptyComment(int userId)
        {
            return await _ratingProductRepository.CountRatingByUserIdAndEmptyComment(userId);
        }

        public async Task<double> CountEmptyCommentByProductId(int productId)
        {
            return await _ratingProductRepository.CountEmptyCommentByProductId(productId);
        }

        public async Task<List<RatingProductDTO>> GetByUserIdAndProductId(int userId, int productId)
        {
            List<RatingProduct> ratingProducts = 
                await _ratingProductRepository.GetByUserIdAndProductId(userId, productId);

            return ratingProducts
                       .Select(RatingProductMapper.MapRatingProductToRatingProductDTO)
                       .ToList();
        }
    }
}
