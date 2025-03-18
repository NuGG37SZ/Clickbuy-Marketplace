using Microsoft.EntityFrameworkCore;
using RatingService.Model.Db;
using RatingService.Model.Entity;

namespace RatingService.Model.Repository
{
    public class RatingSellerRepositoryImpl : IRatingSellerRepository
    {
        private readonly RatingContext _ratingContext;

        public RatingSellerRepositoryImpl(RatingContext ratingContext) => _ratingContext = ratingContext;

        public async Task Create(RatingSeller ratingSeller)
        {
            await _ratingContext.RatingSellers.AddAsync(ratingSeller);
            await _ratingContext.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            RatingSeller? ratingSeller = await GetById(id);

            if (ratingSeller != null)
            {
                _ratingContext.RatingSellers.Remove(ratingSeller);
                await _ratingContext.SaveChangesAsync();
            }
        }

        public async Task<List<RatingSeller>> GetAll()
        {
            return await _ratingContext.RatingSellers.ToListAsync();
        }

        public async Task<RatingSeller?> GetById(int id)
        {
            RatingSeller? ratingSeller = await _ratingContext.RatingSellers.FindAsync(id);

            if (ratingSeller != null)
                return ratingSeller;

            return null;
        }

        public async Task<List<RatingSeller>> GetByUserId(int userId)
        {
            return await _ratingContext.RatingSellers
                                .Where(rs => rs.UserId == userId)
                                .ToListAsync();
        }

        public async Task Update(int id, RatingSeller ratingSeller)
        {
            RatingSeller? currentRatingSeller = await GetById(id);

            if (currentRatingSeller != null)
            {
                currentRatingSeller.Rating = ratingSeller.Rating;
                currentRatingSeller.UserId = ratingSeller.UserId;
                currentRatingSeller.Comment = ratingSeller.Comment;
                currentRatingSeller.DateCreateComment = ratingSeller.DateCreateComment;
                _ratingContext.Update(currentRatingSeller);
                await _ratingContext.SaveChangesAsync();
            }
        }
    }
}
