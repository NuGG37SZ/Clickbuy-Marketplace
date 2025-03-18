namespace RatingService.View.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PointId { get; set; }
        public DateTime CreateOrder { get; set; }
        public DateTime? UpdateOrder { get; set; }
        public string Status { get; set; }
    }
}
