namespace RatingService.View
{
    public class RatingProductView
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int ProductSizesId { get; set; }

        public int UserId { get; set; }

        public int OrderId { get; set; }

        public double Rating { get; set; }

        public string Comment { get; set; }

        public DateTime DateCreateComment { get; set; }
    }
}
