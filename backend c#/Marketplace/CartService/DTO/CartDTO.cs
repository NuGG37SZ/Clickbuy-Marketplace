namespace CartService.DTO
{
    public class CartDTO
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int UserId { get; set; }

        public int ProductSizesId { get; set; }

        public int Count { get; set; }
    }
}
