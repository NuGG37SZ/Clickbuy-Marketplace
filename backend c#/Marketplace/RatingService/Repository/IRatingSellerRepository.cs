using RatingService.Entity;

namespace RatingService.Repository
{
    public interface IRatingSellerRepository
    {
        Task<List<RatingSeller>> GetAll();

        Task<RatingSeller?> GetById(int id);

        Task<List<RatingSeller>> GetByUserId(int userId);

        Task Create(RatingSeller ratingSeller);

        Task Update(int id, RatingSeller ratingSeller);

        Task DeleteById(int id);
    }
}
