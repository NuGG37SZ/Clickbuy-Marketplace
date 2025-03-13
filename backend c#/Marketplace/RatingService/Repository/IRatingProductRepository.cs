using RatingService.Entity;

namespace RatingService.Repository
{
    public interface IRatingProductRepository 
    {
        Task<List<RatingProduct>> GetAll();

        Task<RatingProduct?> GetById(int id);

        Task<List<RatingProduct>> GetByProductId(int productId);

        Task Create(RatingProduct ratingProduct);

        Task Update(int id, RatingProduct ratingProduct);

        Task DeleteById(int id);
    }
}
