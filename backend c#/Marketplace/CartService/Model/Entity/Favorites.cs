namespace CartService.Model.Entity
{
    public class Favorites
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int UserId { get; set; }

        public DateTime DateAdded { get; set; }
    }
}
