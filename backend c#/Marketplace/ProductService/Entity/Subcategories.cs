namespace ProductService.Entity
{
    public class Subcategories
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        public List<Product>? Products { get; set; } = new List<Product>();

        public List<Brand>? Brands { get; set; } = new List<Brand>();

        public string Name { get; set; }
    }
}
