using RatingService.DTO;

namespace RatingService.Service
{
    public interface IRatingProductService
    {
        Task<List<RatingProductDTO>> GetAll();

        Task<RatingProductDTO?> GetById(int id);

        Task<List<RatingProductDTO>> GetByProductId(int productId);

        Task Create(RatingProductDTO ratingProductDTO);

        Task Update(int id, RatingProductDTO ratingProductDTO);

        Task DeleteById(int id);
    }
}
