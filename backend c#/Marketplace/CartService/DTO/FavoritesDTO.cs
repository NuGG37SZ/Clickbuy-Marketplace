namespace CartService.DTO
{
    public class FavoritesDTO
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int UserId { get; set; }

        public DateTime DateAdded { get; set; }
    }
}
