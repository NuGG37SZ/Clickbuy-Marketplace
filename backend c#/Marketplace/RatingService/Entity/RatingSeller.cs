namespace RatingService.Entity
{
    public class RatingSeller
    {
        public int Id { get; set; } 

        public int UserId { get; set; }

        public double Rating { get; set; }

        public string Comment { get; set; }

        public DateTime DateCreateComment { get; set; }
    }
}
