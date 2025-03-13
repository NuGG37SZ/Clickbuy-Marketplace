using RatingService.DTO;

namespace RatingService.Service
{
    public interface IRatingSellerService
    {
        Task<List<RatingSellerDTO>> GetAll();

        Task<RatingSellerDTO?> GetById(int id);

        Task<List<RatingSellerDTO>> GetByUserId(int userId);

        Task Create(RatingSellerDTO ratingSellerDTO);

        Task Update(int id, RatingSellerDTO ratingSellerDTO);

        Task DeleteById(int id);
    }
}
