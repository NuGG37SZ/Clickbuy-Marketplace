using RatingService.Model.Entity;
using RatingService.View;
using RatingService.View.DTO;

namespace RatingService.Model.Mapper
{
    public class RatingProductMapper
    {
        public static RatingProductDTO MapRatingProductToRatingProductDTO(RatingProduct ratingProduct)
        {
            RatingProductDTO ratingProductDTO = new RatingProductDTO();
            ratingProductDTO.Id = ratingProduct.Id;
            ratingProductDTO.ProductId = ratingProduct.ProductId;
            ratingProductDTO.ProductSizesId = ratingProduct.ProductSizesId;
            ratingProductDTO.UserId = ratingProduct.UserId;
            ratingProductDTO.OrderId = ratingProduct.OrderId;
            ratingProductDTO.Rating = ratingProduct.Rating;
            ratingProductDTO.Comment = ratingProduct.Comment;
            ratingProductDTO.DateCreateComment = ratingProduct.DateCreateComment;
            return ratingProductDTO;
        }

        public static RatingProduct MapRatingProductDTOToRatingProduct(RatingProductDTO ratingProductDTO)
        {
            RatingProduct ratingProduct = new RatingProduct();
            ratingProduct.ProductId = ratingProductDTO.ProductId;
            ratingProduct.ProductSizesId = ratingProductDTO.ProductSizesId;
            ratingProduct.UserId = ratingProductDTO.UserId;
            ratingProduct.Rating = ratingProductDTO.Rating;
            ratingProduct.OrderId = ratingProductDTO.OrderId;
            ratingProduct.Comment = ratingProductDTO.Comment;
            ratingProduct.DateCreateComment = ratingProductDTO.DateCreateComment;
            return ratingProduct;
        }

        public static RatingProductView MapRatingProductDTOToRatingProductView(
            RatingProductDTO ratingProductDTO)
        {
            RatingProductView ratingProductView = new RatingProductView();
            ratingProductView.Id = ratingProductDTO.Id;
            ratingProductView.ProductId = ratingProductDTO.ProductId;
            ratingProductView.ProductSizesId = ratingProductDTO.ProductSizesId;
            ratingProductView.UserId = ratingProductDTO.UserId;
            ratingProductView.OrderId = ratingProductDTO.OrderId;
            ratingProductView.Rating = ratingProductDTO.Rating;
            ratingProductView.Comment = ratingProductDTO.Comment;
            ratingProductDTO.DateCreateComment = ratingProductDTO.DateCreateComment;
            return ratingProductView;
        }

        public static List<RatingProductView> MapRatingProductDTOListToRatingProductViewList(
            List<RatingProductDTO> ratingProductDTOList)
        {
            return ratingProductDTOList.Select(MapRatingProductDTOToRatingProductView).ToList();
        }

    }
}
