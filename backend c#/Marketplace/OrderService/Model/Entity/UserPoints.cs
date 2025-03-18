namespace OrderService.Model.Entity
{
    public class UserPoints
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int PointsId { get; set; }

        public bool IsActive { get; set; }
    }
}
