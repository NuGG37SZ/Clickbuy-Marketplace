namespace RatingService.View.DTO
{
    public class RatingSellerDTO
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public double Rating { get; set; }

        public string Comment { get; set; }

        public DateTime DateCreateComment { get; set; }
    }
}
