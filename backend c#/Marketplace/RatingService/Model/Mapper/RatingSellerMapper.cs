using RatingService.Model.Entity;
using RatingService.View.DTO;

namespace RatingService.Model.Mapper
{
    public class RatingSellerMapper
    {
        public static RatingSellerDTO MapRatingSellerToRatingSellerDTO(RatingSeller ratingSeller)
        {
            RatingSellerDTO ratingSellerDTO = new RatingSellerDTO();
            ratingSellerDTO.Id = ratingSeller.Id;
            ratingSellerDTO.Rating = ratingSeller.Rating;
            ratingSellerDTO.UserId = ratingSeller.UserId;
            ratingSellerDTO.Comment = ratingSeller.Comment;
            ratingSellerDTO.DateCreateComment = ratingSeller.DateCreateComment;
            return ratingSellerDTO;
        }

        public static RatingSeller MapRatingSellerDTOToRatingSeller(RatingSellerDTO ratingSellerDTO)
        {
            RatingSeller ratingSeller = new RatingSeller();
            ratingSeller.Rating = ratingSellerDTO.Rating;
            ratingSeller.UserId = ratingSellerDTO.UserId;
            ratingSeller.Comment = ratingSellerDTO.Comment;
            ratingSeller.DateCreateComment = ratingSellerDTO.DateCreateComment;
            return ratingSeller;
        }
    }
}
