namespace ProductService.Entity
{
    public class ProductSizes
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public Product? Product { get; set; }

        public string Size { get; set; }

        public int Count { get; set; }
    }
}
