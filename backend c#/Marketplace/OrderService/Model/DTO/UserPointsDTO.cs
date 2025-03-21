namespace OrderService.Model.DTO
{
    public class UserPointsDTO
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int PointsId { get; set; }

        public bool IsActive { get; set; }
    }
}
