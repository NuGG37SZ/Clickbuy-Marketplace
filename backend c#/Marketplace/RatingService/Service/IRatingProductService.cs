using RatingService.DTO;

namespace RatingService.Service
{
    public interface IRatingProductService
    {
        Task<List<RatingProductDTO>> GetAll();

        Task<RatingProductDTO?> GetById(int id);

        Task<RatingProductDTO?> GetByProductIdAndProductSizesIdAndOrderId(int productId, 
            int productSizesId, int orderId);

        Task<List<RatingProductDTO>> GetByProductId(int productId);

        Task<List<RatingProductDTO>> GetByUserId(int userId);

        Task<double> AvgRatingByProductId(int productId);

        Task<double> CountRatingByUserIdAndEmptyComment(int userId);

        Task Create(RatingProductDTO ratingProductDTO);

        Task Update(int id, RatingProductDTO ratingProductDTO);

        Task DeleteById(int id);
    }
}
