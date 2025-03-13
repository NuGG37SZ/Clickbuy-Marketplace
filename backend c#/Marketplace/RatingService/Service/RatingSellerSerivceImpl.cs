using RatingService.DTO;
using RatingService.Entity;
using RatingService.Mapper;
using RatingService.Repository;

namespace RatingService.Service
{
    public class RatingSellerSerivceImpl : IRatingSellerService
    {
        private readonly IRatingSellerRepository _ratingSellerRepository;

        public RatingSellerSerivceImpl(IRatingSellerRepository ratingSellerRepository) => 
            _ratingSellerRepository = ratingSellerRepository;

        public async Task Create(RatingSellerDTO ratingSellerDTO)
        {
            await _ratingSellerRepository.Create(
                RatingSellerMapper.MapRatingSellerDTOToRatingSeller(ratingSellerDTO)
            );
        }

        public async Task DeleteById(int id)
        {
            RatingSellerDTO? ratingSellerDTO = await GetById(id);

            if (ratingSellerDTO != null) 
                await _ratingSellerRepository.DeleteById(id);
        }

        public async Task<List<RatingSellerDTO>> GetAll()
        {
            List<RatingSeller> ratingSellers = await _ratingSellerRepository.GetAll();

            return ratingSellers
                    .Select(RatingSellerMapper.MapRatingSellerToRatingSellerDTO)
                    .ToList();
        }

        public async Task<RatingSellerDTO?> GetById(int id)
        {
            RatingSeller? ratingSeller = await _ratingSellerRepository.GetById(id);

            if(ratingSeller != null)
                return RatingSellerMapper.MapRatingSellerToRatingSellerDTO(ratingSeller);

            return null;
        }

        public async Task<List<RatingSellerDTO>> GetByUserId(int userId)
        {
            List<RatingSeller> ratingSellers = await _ratingSellerRepository.GetByUserId(userId);

            return ratingSellers
                    .Select(RatingSellerMapper.MapRatingSellerToRatingSellerDTO)
                    .ToList();
        }

        public async Task Update(int id, RatingSellerDTO ratingSellerDTO)
        {
            RatingSellerDTO? currentRatingSellerDTO = await GetById(id);

            if (currentRatingSellerDTO != null)
            {
                await _ratingSellerRepository.Update(id, 
                    RatingSellerMapper.MapRatingSellerDTOToRatingSeller(ratingSellerDTO));
            }
                
        }
    }
}
