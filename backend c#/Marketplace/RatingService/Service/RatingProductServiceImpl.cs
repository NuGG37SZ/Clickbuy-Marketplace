using RatingService.DTO;
using RatingService.Entity;
using RatingService.Mapper;
using RatingService.Repository;

namespace RatingService.Service
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

            if( ratingProductDTO != null ) 
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

            if(ratingProduct != null )
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

        public async Task Update(int id, RatingProductDTO ratingProductDTO)
        {
            RatingProductDTO? currentRatingProductDTO = await GetById(id);

            if (currentRatingProductDTO != null)
            {
                await _ratingProductRepository.Update(id, 
                    RatingProductMapper.MapRatingProductDTOToRatingProduct(ratingProductDTO));
            }
        }
    }
}
